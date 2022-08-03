<template>
  <div class="m-row">
    <div id="m-toast-box"></div>
  </div>
</template>
<script>
export default {
  name: "ToastMsg",
  props: {
    // toast lên icon success
    Status: {
      default: "",
      required: true,
    },
    // add thêm chuỗi thành công vào Toast
    Msg: {
      default: "",
      required: true,
    },
  },
  computed: {
    compoundProperty: function () {
      return [this.Msg, this.Status];
    },
  },
  watch: {
    compoundProperty: function () {
      let templateInner =
        `<div class="m-toast-icon">
          <i class="fas fa-exclamation-circle"></i>
        </div>
        <div class="m-toast-text">` +
        this.Msg +
        `</div>
        <div class="m-toast-close">
          <i class="fas fa-times"></i>
        </div>`;
      switch (this.Status) {
        case "m-toast-icon-error":
          templateInner =
            `<div class="m-icon m-notification-error mi-24">
                          </div>
                          <div class="m-toast-text">` +
            this.Msg +
            `</div>
                          <div class="m-toast-close">
                            <i class="fas fa-times"></i>
                          </div>`;
          break;
        case "m-toast-icon-warning":
          templateInner =
            `<div class="m-icon m-exclamation-warning mi-24">
                          </div>
                          <div class="m-toast-text">` +
            this.Msg +
            `</div>
                          <div class="m-toast-close">
                            <i class="fas fa-times"></i>
                          </div>`;
          break;
        case "m-toast-icon-success":
          templateInner =
            `<div class="m-icon m-exclamation-success mi-24">
                          </div>
                          <div class="m-toast-text">` +
            this.Msg +
            `</div>
                          <div class="m-toast-close">
                            <i class="fas fa-times"></i>
                          </div>`;
          break;
        case "m-toast-icon-info":
          templateInner =
            `<div class="m-icon m-exclamation-question mi-24">
                          </div>
                          <div class="m-toast-text">` +
            this.Msg +
            `</div>
                          <div class="m-toast-close">
                            <i class="fas fa-times"></i>
                          </div>`;
          break;

        default:
          break;
      }
      // lấy ra thẻ div add class toast msg + add icon success
      var toast = document.createElement("div");
      toast.classList.add("m-toast-msg");
      toast.classList.add(this.Status);
      toast.innerHTML = templateInner;

      var toastList = document.getElementById("m-toast-box");
      toastList.append(toast);

      // tạp animation cho Toast
      setTimeout(function () {
        toast.style.animation = "slide_hide 2s ease forwards";
      }, 3000);
      // đóng toast sau 5s xuất hiện
      setTimeout(function () {
        toast.remove();
      }, 5000);
    },
  },
  methods: {},
  data() {
    return {
      msgToast: null,
      statusToast: null,
    };
  },
};
</script>