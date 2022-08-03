// B1: Cài đặt vue-router
// B2: import
import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from "../views/employee/EmployeeList.vue";
import TheDashboard from "../views/dashboard/TheDashboard.vue"
const routes = [
  //   { path: "/", component: Home },
  { path: "/employee", component: EmployeeList },
  { path: "/dashboard", component: TheDashboard },
];

const router = createRouter({
  history: createWebHistory(),
  routes, // short for `routes: routes`
});

export default router;
