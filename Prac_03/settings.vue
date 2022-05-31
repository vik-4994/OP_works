<template>
  <div>
    <el-form
      :model="controls"
      :rules="rules"
      ref="form"
      @submit.native.prevent="onSubmitPassword"
      class="center"
    >
      <h2>Изменить пароль</h2>

      <div class="mb2">
        <el-form-item label="Пароль" prop="password">
          <el-input v-model.trim="controls.password" type="password" />
        </el-form-item>
      </div>

      <div class="mb2">
        <el-form-item label="Подтвердить пароль" prop="confirmPassword">
          <el-input v-model.trim="controls.confirmPassword" type="password" />
        </el-form-item>
      </div>

      <div class="mb2">
        <el-form-item label="Введите старый пароль" prop="oldPassword">
          <el-input v-model.trim="controls.oldPassword" type="password" />
        </el-form-item>
      </div>

      <el-form-item>
        <el-button type="text" native-type="submit" round :loading="loading">
          Изменить пароль
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import Cookie from "cookie";
import jwtDecode from "jwt-decode";
export default {
  head: {
    title: `Настройки | ${process.env.appName}`,
  },
  layout: "admin",
  middleware: ["admin-auth"],
  data() {
    let validateComfirmPwd = (rule, value, callback) => {
      if (this.controls.confirmPassword !== this.controls.password) {
        callback(new Error("Пароли не совпадают"));
      } else {
        callback();
      }
    };
    return {
      loading: false,
      controls: {
        password: "",
        confirmPassword: "",
        oldPassword: ""
      },
      rules: {
        password: [
          { required: true, pattern: /^[a-zA-Z0-9.·-]+$/ ,message: "Введите пароль", trigger: "blur" },
          {
            min: 6,
            message: "Пароль должен быть не менее 6 символов",
            trigger: "blur",
          },
        ],
        confirmPassword: [
          { required: true, message: "Подтвердите пароль", trigger: "blur" },
          { validator: validateComfirmPwd, trigger: "blur" },
        ],
        oldPassword: [
          {required: true, message: "Введите старый пароль", trigger: "blue"}
        ]
      },
    };
  },
  methods: {
    onSubmitPassword() {
      this.$refs.form.validate(async (valid) => {
        if (valid) {
          this.loading = true;
          try {
            const cookieStr = process.browser
              ? document.cookie
              : this.app.context.req.headers.cookie;

            const cookies = Cookie.parse(cookieStr || "") || {};
            const token = cookies["jwt-token"];
            const jwtData = jwtDecode(token) || {};
            const formData = {
              password: this.controls.password,
              login: jwtData.login,
              oldPassword: this.controls.oldPassword
            };
            await this.$store.dispatch("admin/changePassword", formData);
            this.$message.success("Успешно!");

            this.controls.password = "";
            this.controls.confirmPassword = "";
            this.controls.oldPassword = "";
            this.loading = false;
          } catch (e) {
            this.loading = false;
          }
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
form {
  width: 600px;
}

.center {
  margin: 0 auto;
}
</style>
