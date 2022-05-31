<template>
  <div>
    <el-form
      :model="controls"
      :rules="rules"
      ref="form"
      @submit.native.prevent="onSubmit"
    >
      <h2>Создать пользователя</h2>

      <el-form-item label="Логин" prop="login">
        <el-input v-model.trim="controls.login" />
      </el-form-item>

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

      <el-form-item>
        <el-button type="text" native-type="submit" round :loading="loading">
          Создать
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  head: {
    title: `Регистрация | ${process.env.appName}`,
  },
  layout: "empty",
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
        login: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      rules: {
        login: [{ required: true, message: "Введите логин", trigger: "blur" }],
        password: [
          { required: true, pattern: /^[a-zA-Z0-9.·-]+$/, message: "Введите пароль корректно", trigger: "blur" },
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
      },
    };
  },
  methods: {
    onSubmit() {
      this.$refs.form.validate(async (valid) => {
        if (valid) {
          this.loading = true;
          try {
            const formData = {
              login: this.controls.login,
              password: this.controls.password,
              role: this.controls.role,
            };
            await this.$store.dispatch("auth/createUser", formData);
            this.$message.success("Успешно!");
            this.$router.push("/user");
            this.controls.login = "";
            this.controls.password = "";
            this.loading = false;
          } catch (e) {
            this.loading = false;
          }
        }
      });
    },
    chooseRole(payload) {
      this.controls.role = payload;
    },
  },
};
</script>

<style lang="scss" scoped>
form {
  width: 600px;
}
</style>
