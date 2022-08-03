<template>
  <!-- THông báo xác nhận xóa -->
  <div
    class="m-dialog m-dialog-notice m-dialog-delete"
    :class="{ 'm-dialog-show': ShowConfirmNotice }"
  >
    <div class="m-dialog-content">
      <div class="m-dialog-body" style="width: auto">
        <div class="m-icon-notice m-icon m-exclamation-question mi-48"></div>
        <div class="m-message-content">
          Bạn có thực sự muốn xóa nhân viên &#60;{{ EmployeeCode }}&#62; không?
        </div>
      </div>
      <div class="m-notice-mess-line"></div>
      <div class="m-dialog-footer m-notice-footer m-notice-confirm-footer">
        <button
          class="m-button m-btn-nobg m-btn-close-dialog"
          @click="btnCancelOnClick"
          style="width: auto"
        >
          Không
        </button>
        <button
          id="btnDeleteAccept"
          class="m-button m-accept-delete"
          @click="btnDeleteOnClick"
        >
          Có
        </button>
      </div>
    </div>
  </div>
  <ToastSuccess
    :Status="statusToastMsg"
    :Msg="messageToast"
    :CheckChange="change"
  />
</template>
<script>
import axios from "axios";
import config from "../../../config"
export default {
  name: "ConfirmNotice",
  props: {
    ShowConfirmNotice: {
      default: false,
      type: Boolean,
      required: true,
    },
    EmployeeId: {
      default: "",
      required: true,
    },
    EmployeeCode: {
      default: "",
      required: true,
    },
  },
  methods: {
    /**
     * hàm cập nhật trạng thái đóng thông báo xác nhận
     * @author: NQL (31/5/2022)
     */
    btnCancelOnClick() {
      this.$emit("callParent", false);
    },
    /**
     * hàm xóa nhân viên được chọn
     * @author: NQL (31/5/2022)
     */
    btnDeleteOnClick() {
      let me = this;
      axios
        .delete(config.API_EMPLOYEES + me.EmployeeId)
        .then((res) => {
          this.$emit("callParent", false);
          this.$emit("loadData");
          me.messageToast = "Xóa thành công nhân viên";
                me.statusToastMsg = "m-toast-icon-success";
          console.log(res);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  data() {
    return {
      IsShow: false,
      statusToastMsg: null,
      messageToast: null,
      change: false,
    };
  },
};
</script>