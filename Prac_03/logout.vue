<template>
  <div>Очищаем данные</div>
</template>

<script>
export default {
  layout: "admin",
  middleware: ["admin-auth"],
  beforeCreate() {
    this.$store.dispatch("auth/logout");
    this.$router.push("/?message=logout");
  },
};
</script>
