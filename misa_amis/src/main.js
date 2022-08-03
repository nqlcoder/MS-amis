import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import DialogNotice from "./components/base/DialogNotice.vue";
import DialogSuccess from "./components/base/DialogSuccess.vue";
import ConfirmNotice from "./components/base/ConfirmNotice.vue";
import PageLoading from "./components/base/TheLoading.vue";
import ToastSuccess from "./components/base/ToastSuccess.vue";
import Paginate from "vuejs-paginate-next";

const app = createApp(App);

// app.component("DialogNotice", DialogNotice).component("ConfirmNotice", ConfirmNotice).component("PageLoading", PageLoading).component("ToastMsg", ToastMsg);
app.component("DialogNotice", DialogNotice).component("ConfirmNotice", ConfirmNotice).component("PageLoading", PageLoading).component("DialogSuccess", DialogSuccess).component("ToastSuccess", ToastSuccess).component("Paginate", Paginate);
app.config.globalProperties.DialogNotice = DialogNotice.methods;
app.config.globalProperties.ConfirmNotice = ConfirmNotice.methods;
app.config.globalProperties.DialogSuccess = DialogSuccess.methods;
app.config.globalProperties.ToastSuccess = ToastSuccess.methods;
app.use(router).mount("#app");