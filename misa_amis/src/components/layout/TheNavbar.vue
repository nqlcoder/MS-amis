<template>
    <div class="m-navbar">
      <router-link class="m-navbar-item" to="/dashboard">
        <div class="m-navbar-icon m-icon m-icon-dashboard mi-24"></div>
        <div class="m-navbar-text">Tổng quan</div>
      </router-link>

      <router-link class="m-navbar-item" to="/employee">
        <div class="m-navbar-icon m-icon m-icon-cash mi-24"></div>
        <div class="m-navbar-text">Tiền mặt</div>
      </router-link>

      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-bank mi-24"></div>
        <div class="m-navbar-text">Tiền gửi</div>
      </a>

      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-pu mi-24"></div>
        <div class="m-navbar-text">Mua hàng</div>
      </a>

      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-sale mi-24"></div>
        <div class="m-navbar-text">Bán hàng</div>
      </a>

      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-invoice mi-24"></div>
        <div class="m-navbar-text">Quản lý hóa đơn</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-stock mi-24"></div>
        <div class="m-navbar-text">Kho</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-tools mi-24"></div>
        <div class="m-navbar-text">Công cụ dụng cụ</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-fixed-assets mi-24"></div>
        <div class="m-navbar-text">Tài sản cố định</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-tax mi-24"></div>
        <div class="m-navbar-text">Thuế</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-price mi-24"></div>
        <div class="m-navbar-text">Giá thành</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-general mi-24"></div>
        <div class="m-navbar-text">Tổng hợp</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-budget mi-24"></div>
        <div class="m-navbar-text">Ngân sách</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-report mi-24"></div>
        <div class="m-navbar-text">Báo cáo</div>
      </a>
      <a class="m-navbar-item">
        <div class="m-navbar-icon m-icon m-icon-finance mi-24"></div>
        <div class="m-navbar-text">Phân tích tài chính</div>
      </a>
    </div>
</template>

<style scoped>
@import url("../../styles/layout/navbar.css");
</style>

<script>
export default {
    
}
</script>