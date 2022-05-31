<template>
  <div>
    <h1>Аналитика по пользователям</h1>
    <app-analytics-chart
      title="Количество пользователей"
      :labels="['Пользователи', 'Активные пользователи']"
      :data="[accounts.accounts.length, accounts.activeAccounts.length]"
    ></app-analytics-chart>
  </div>
</template>

<script>
import AppAnalyticsChart from "@/components/admin/AnalyticChart";
export default {
  head: {
    title: `Аналитика | ${process.env.appName}`,
  },
  async asyncData({ store }) {
    const accounts = await store.dispatch("post/getAccounts");
    return { accounts };
  },
  layout: "admin",
  components: { AppAnalyticsChart },
  middleware: ["admin-auth"],
};
</script>

