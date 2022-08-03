<template>
  <!-- THông báo lỗi -->
  <div
    class="m-dialog m-dialog-notice"
    :class="{ 'm-dialog-show': ShowNoticeDialog }"
  >
    <div class="m-dialog-content">
      <div class="m-dialog-body" style="width: auto">
        <div id="icon-notice" class="m-icon-notice m-icon mi-48 m-notification-error"></div>
        <!-- m-exclamation-error -->
        <div class="m-message-content"></div>
      </div>
      <div class="m-notice-mess-line"></div>
      <div class="m-dialog-footer m-notice-footer">
        <button class="m-button m-close-notice" @click="btnConfirmOnClick">
          Đóng
        </button>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "DialogNotice",
  props: {
    StatusNotice:{
      default: [],
      required: true,
    },
    Msgs: {
      default: [],
      required: true,
    },
    ShowNoticeDialog: {
      default: false,
      type: Boolean,
      required: true,
    },
  },
  watch: {
    ShowNoticeDialog: function (newValue) {
      if (newValue) {
        this.buildMsgNotice(this.Msgs);
      }
      console.log(newValue);
      this.IsShow = newValue;
    },
  },
  methods: {
    buildMsgNotice(validateErrors) {
      // Hiển thị cảnh báo không hợp lệ
      let noticeDialog = document.getElementsByClassName("m-dialog-notice")[0];
      let bodyTextNoticeDialog =
        noticeDialog.querySelector(".m-message-content");
      bodyTextNoticeDialog.innerHTML = "";

      //Append nội dung thông báo
      // document.getElementById("icon-notice").classList.add(this.StatusNotice);
      validateErrors.forEach((errorMsg) => {
        var el = document.createElement("div");
        el.innerText = `- ${errorMsg}`;
        bodyTextNoticeDialog.append(el);
      });
    },
    btnConfirmOnClick() {
      this.$emit("callParent", false);
    },
  },
  data() {
    return {
      IsShow: false,
    };
  },
};
</script>