<template>
  <div>
    <el-table :data="users" style="width: 100%">
      <el-table-column label="Логин">
        <template slot-scope="{ row: { login } }">
          <span style="margin-left: 5px">{{ login }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Роль">
        <template slot-scope="{ row: { role } }">
          <span style="margin-left: 2px">{{ role }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Статус">
        <template slot-scope="{ row: { status } }">
          <span style="margin-left: 2px">{{ status }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Дата создания">
        <template slot-scope="{ row: { dateOfCreation } }">
          <span style="margin-left: 2px">{{
            new Date(dateOfCreation).toLocaleString()
          }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Действия">
        <template slot-scope="{ row }">
          <el-tooltip v-if="row.role !== 'admin' && row.status !== 'banned'"
            effect="dark"
            content="Забанить пользователя"
            placement="top"
          >
            <el-button
              icon="el-icon-remove-outline"
              type="primary"
              circle
              @click="ban(row._id, row.login)"
            />
          </el-tooltip>
          <el-tooltip effect="dark" content="Удалить аккаунт" placement="top" v-if="row.role !== 'admin'">
            <el-button
              icon="el-icon-delete"
              type="danger"
              circle
              @click="remove(row._id)"
            />
          </el-tooltip>
          <el-tooltip v-if="row.role !== 'admin'"
            effect="dark"
            content="Сменить логин"
            placement="top"
          >
            <el-button
              icon="el-icon-edit"
              type="primary"
              circle
              @click="openDialog(row._id)"
            />
          </el-tooltip>
        </template>
      </el-table-column>
    </el-table>
    <el-dialog
    :visible.sync="dialogVisible"
    width="30%"
    height="200px">
    <el-form
      :model="newLogin"
      ref="form1"
      class="center"
    >
      <el-form-item label="Изменить логин" prop="name">
        <el-input v-model.trim="newLogin" />
      </el-form-item>
    </el-form>
    <span slot="footer" class="dialog-footer">
      <el-button @click="dialogVisible = false">Cancel</el-button>
      <el-button type="primary" @click="rename(currentId, newLogin)">Confirm</el-button>
    </span>
  </el-dialog>
  </div>
</template>

<script>
export default {
  head: {
    title: `Аккаунты | ${process.env.appName}`,
  },
  layout: "admin",
  middleware: ["admin-auth"],
  async asyncData({ store }) {
    const users = await store.dispatch("admin/getUsers");
    return { users, dialogVisible: false, currentId: "", newLogin: ""};
  },
  computed: {
    error() {
      return this.$store.getters.error;
    },
  },
  watch: {
    error(value) {
      this.$message.error(value.response.data.message);
    },
  },
  methods: {
    open(id) {
      this.$router.push(`/admin/post/${id}`);
    },
    async remove(id) {
      try {
        await this.$confirm("Удалить аккаунт?", "Внимание!", {
          confirmButtonText: "Да",
          cancelButtonText: "Отменить",
          type: "warning",
        });
        await this.$store.dispatch("auth/deleteUser", { id });
        this.users = this.users.filter((p) => p._id !== id);
        this.$message.success("Аккаунт удален");
      } catch (e) {}
    },
    async ban(id, login) {
      try {
        await this.$confirm("Забанить пользователя?", "Внимание!", {
          confirmButtonText: "Да",
          cancelButtonText: "Отменить",
          type: "warning",
        });

        await this.$store.dispatch("admin/ban", { id, login });
        this.$message.success("Пользователь забанен");
      } catch (e) {}
    },
    openDialog(id){
      this.currentId = id;
      this.dialogVisible = true;
    },
    async rename(id, login){
      try {
        await this.$confirm("Изменить логин пользователя?", "Внимание!", {
          confirmButtonText: "Да",
          cancelButtonText: "Отменить",
          type: "warning",
        });

        await this.$store.dispatch("admin/rename", { id, login });
        this.$message.success("Логин изменён");
      } catch (e) {}
      this.dialogVisible = false;
    }
  },
};
</script>
