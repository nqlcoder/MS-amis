<template>
  <!-- Dialog thông tin nhân viên -->
  <!-- add class để chuyển đổi động giữa các component -->
  <!-- v-model: tạo ràng buộc 2 chiều trên phần wtr đầu vào -->
  <div
    id="dlgEmployeeDetail"
    class="m-dialog"
    :class="{ 'm-dialog-show': IsShowDialogChild }"
  >
    <div class="m-dialog-content m-info-employee">
      <div class="m-dialog-header">
        <div class="m-header-container">
          <div class="title m-dialog-title">Thông tin nhân viên</div>
          <div class="m-is-customer">
            <input
              type="checkbox"
              id="isCustomer"
              name="isCustomer"
              value="isCustomer"
            />
            <label for="isCustomer" style="padding-left: 5px"
              >Là khách hàng</label
            >
          </div>
          <div class="m-is-provider">
            <input
              type="checkbox"
              id="isProvider"
              name="isProvider"
              value="isProvider"
            />
            <label for="isProvider" style="padding-left: 5px"
              >Là nhà cung cấp</label
            >
          </div>
        </div>
        <div class="m-dialog-close">
          <div
            class="m-help-dialog m-icon m-icon-help mi-24"
            style="margin-right: 6px"
          ></div>
          <div
            class="m-close-dialog m-icon m-icon-close mi-24"
            @click="btnCloseDialog"
          ></div>
        </div>
      </div>
      <div class="m-dialog-body">
        <div class="m-dialog-body-content1">
          <div class="m-dialog-body1-left">
            <div class="m-flex-row-input">
              <div class="m-row" style="padding-right: 6px; width: 250px">
                <label class="m-label" for=""
                  >Mã <span class="required">*</span></label
                >
                <div class="m-row">
                  <input
                    propertyName="Mã nhân viên"
                    type="text"
                    id="txtEmpCode"
                    class="m-input"
                    @onKeyup="
                    (value) => {
                      dataChange('employeeCode', value);
                    }
                  "
                    v-model="employee.employeeCode"
                    required
                  />
                </div>
              </div>

              <div class="m-row">
                <label class="m-label" for=""
                  >Tên <span class="required">*</span></label
                >
                <div class="m-row">
                  <input
                    propertyName="Tên nhân viên"
                    type="text"
                    id="txtFullName"
                    class="m-input"
                    @onKeyup="
                    (value) => {
                      dataChange('fullName', value);
                    }
                  "
                    v-model="employee.fullName"
                    required
                  />
                </div>
              </div>
            </div>

            <div class="m-flex-row-input">
              <div class="m-row">
                <label class="m-label" for=""
                  >Đơn vị <span class="required">*</span></label
                >

                <div class="m-row">
                  <select
                    id="cbxDepartment"
                    class="m-select-department"
                    @onKeyup="
                    (value) => {
                      dataChange('departmentId', value);
                    }
                  "
                    v-model="employee.departmentId"
                    required
                  >
                    <option
                      v-for="department in departments"
                      v-bind:key="department.departmentId"
                      v-bind:value="department.departmentId"
                    >
                      {{ department.departmentName }}
                    </option>
                  </select>
                </div>
              </div>
            </div>

            <div class="m-flex-row-input">
              <div class="m-row">
                <label class="m-label" for="">Chức danh</label>
                <div class="m-row">
                  <input
                    propertyName="Chức danh"
                    type="text"
                    id="txtPosition"
                    v-model="employee.positionName"
                    class="m-input"
                  />
                </div>
              </div>
            </div>
          </div>

          <div class="m-dialog-body1-right">
            <div class="m-flex-row-input">
              <div class="m-row" style="width: 250px; padding-right: 16px">
                <label class="m-label" for="">Ngày sinh</label>
                <div class="m-row">
                  <input
                    style="position: relative"
                    propertyName="Ngày sinh"
                    formatDate="DD/MM/YYYY"
                    type="date"
                    id="dateDateOfBirth"
                    class="m-input"
                    @onKeyup="
                    (value) => {
                      dataChange('dateOfBirth', value);
                    }
                  "
                    v-model="employee.dateOfBirth"
                  />
                  <span class="m-icon mi-24 mi-datepicker"></span>
                </div>
              </div>

              <div class="m-row">
                <label class="m-label" for="">Giới tính</label>
                <div class="m-row m-radio-gender" style="margin-top: 8px">
                  <input
                    style="width: 20px; height: 16px"
                    type="radio"
                    id="nam"
                    name="gender"
                    :value="1"
                    v-model.number="employee.gender"
                  />
                  <label for="1" style="padding-right: 10px; padding-left: 5px"
                    >Nam</label
                  ><br />
                  <input
                    style="width: 20px; height: 16px"
                    type="radio"
                    id="nữ"
                    name="gender"
                    :value="0"
                    v-model.number="employee.gender"
                  />
                  <label for="0" style="padding-right: 10px; padding-left: 5px"
                    >Nữ</label
                  ><br />
                  <input
                    style="width: 20px; height: 16px"
                    type="radio"
                    id="khác"
                    name="gender"
                    :value="2"
                    v-model.number="employee.gender"
                  />
                  <label for="2" style="padding-right: 10px; padding-left: 5px"
                    >Không xác định</label
                  >
                </div>
              </div>
            </div>

            <div class="m-flex-row-input">
              <div class="m-row" style="padding-right: 6px">
                <label class="m-label" for="">Số CMND</label>
                <div class="m-row">
                  <input
                    propertyName="Số CMND"
                    type="text"
                    id="identityNumber"
                    v-model="employee.identityNumber"
                    class="m-input"
                  />
                </div>
              </div>
              <div class="m-row" style="width: 250px; padding-right: 16px">
                <label class="m-label" for="">Ngày cấp</label>
                <div class="m-row">
                  <input
                    style="position: relative"
                    propertyName="Ngày cấp"
                    formatDate="DD/MM/YYYY"
                    type="date"
                    id="identityDate"
                    class="m-input"
                    v-model="employee.identityDate"
                  />
                  <span class="m-icon mi-24 mi-datepicker"></span>
                </div>
              </div>
            </div>

            <div class="m-flex-row-input">
              <div class="m-row">
                <label class="m-label" for="">Nơi cấp</label>
                <div class="m-row">
                  <input
                    propertyName="Nơi cấp"
                    type="text"
                    id="identityPlace"
                    v-model="employee.identityPlace"
                    class="m-input"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="m-dialog-body-content2">
          <div class="m-flex-row-input">
            <div class="m-row">
              <label class="m-label" for="">Địa chỉ</label>
              <div class="m-row">
                <input
                  propertyName="Địa chỉ"
                  type="text"
                  id="address"
                  v-model="employee.address"
                  class="m-input"
                />
              </div>
            </div>
          </div>

          <div class="m-flex-row-input" style="width: auto">
            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">DT di động</label>
              <div class="m-row">
                <input
                  propertyName="Điện thoại di động"
                  type="text"
                  id="mobilePhone"
                  v-model="employee.mobile"
                  class="m-input"
                />
              </div>
            </div>

            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">DT cố định</label>
              <div class="m-row">
                <input
                  propertyName="Điện thoại cố định"
                  type="text"
                  id="telephoneNumber"
                  v-model="employee.phoneNumber"
                  class="m-input"
                />
              </div>
            </div>

            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">Email</label>
              <div class="m-row">
                <input
                  propertyName="Email"
                  type="text"
                  id="email"
                  v-model="employee.email"
                  @blur="EmailBlur($event)"
                  class="m-input"
                />
              </div>
            </div>
          </div>

          <div class="m-flex-row-input" style="width: auto">
            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">Tài khoản ngân hàng</label>
              <div class="m-row">
                <input
                  propertyName="Tài khoản ngân hàng"
                  type="text"
                  id="bankAccountNumber"
                  v-model="employee.bankAccountNumber"
                  class="m-input"
                />
              </div>
            </div>

            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">Tên ngân hàng</label>
              <div class="m-row">
                <input
                  propertyName="Tên ngân hàng"
                  type="text"
                  id="bankName"
                  v-model="employee.bankName"
                  class="m-input"
                />
              </div>
            </div>

            <div
              class="m-row"
              style="padding-right: 6px; width: 200px; min-width: 200px"
            >
              <label class="m-label" for="">Chi nhánh</label>
              <div class="m-row">
                <input
                  propertyName="Chi nhánh"
                  type="text"
                  id="bankBranchname"
                  v-model="employee.bankBranchName"
                  class="m-input"
                />
              </div>
            </div>
          </div>
        </div>

        <div class="m-divide"></div>
      </div>
      <div class="m-dialog-footer">
        <div class="m-dialog-footer-left">
          <button
            id="btnCloseDlg"
            class="m-button m-btn-nobg m-btn-close-dialog"
            @click="btnCloseDialog"
          >
            Hủy
          </button>
        </div>
        <div class="m-dialog-footer-right">
          <button
            id="btnSave"
            class="m-button m-btn-nobg m-btn-save"
            style="margin-right: 10px"
            @click="btnSaveOnClick"
          >
            Cất
          </button>
          <button id="btnSaveAdd" class="m-button" @click="btnSaveAddOnClick">
            Cất và Thêm
          </button>
        </div>
      </div>
    </div>
  </div>
  <!-- THông báo lỗi -->
  <DialogNotice
    :ShowNoticeDialog="ShowNoticeDialog"
    :Msgs="ValidateErrors"
    @callParent="showOrHideDialogNotice"
  />

  <DialogSuccess
    :ShowSuccessDialog="ShowSuccessDialog"
    :Msgs="ValidateSuccess"
    @callParent="showOrHideDialogSuccess"
  />
  <!-- Toast thông báo thành công -->
  <ToastSuccess :Status="statusToastMsg" :Msg="messageToast" />

  <!-- confirm -->
   <!-- Xác nhận xóa -->
  <ConfirmNotice
    :ShowConfirmNotice="showConfirmNotice"
    :EmployeeId="employeeIdDelete"
    :EmployeeCode="employeeCodeDelete"
    @callParent="showOrHideConfirmNotice"
    @loadData="btnRefreshOnClick"
  />
</template>
<script>
import axios from "axios";
import config from "../../../config";
import enumProperties from "../../../enumProperties";
// import enum from "../../../enum";
export default {
  name: "EmployeeDetail",
  created() {
    var me = this;

    axios.get(config.API_NEWEMPLOYEECODE).then(function (res) {
      var newEmployeeCode = res.data;
      //Hiển thị dialog
      me.IsShowDialog = true;
      //Gán giá trị cho ô Employee Code
      me.employee.employeeCode = newEmployeeCode;

      // 2. Focus vào ô nhập liệu đầu tiên
      me.$nextTick(function () {
        //hàm nextTick đảm bảo sau khi Render hết thì mới thực hiện Function trong hàm
        document.getElementById("txtEmpCode").focus();
      }).catch(function (res) {
        console.log(res);
      });
    });
  },
  async mounted() {
    /**
     * Thực hiện lấy dữ liệu về Department ở giai đoạn mounted
     * author: NQL (31/5/2022)
     */
    try {
      var me = this;
      await axios.get(config.API_DEPARTMENTS).then((res) => {
        // console.log(res.data);
        me.departments = res.data;
      });
    } catch (error) {
      console.log(error);
    }
  },
  props: {
    // editMode = 1: Thêm, editMode = 0: Sửa
    editMode: {
      default: 0,
      type: Number,
      required: true,
    },
    // chuyển đổi động giwca các lớp
    IsShowDialogChild: {
      default: false,
      type: Boolean,
      required: true,
    },
    employeeInfo: {
      type: Object,
      required: true,
    },
  },
  watch: {
    employeeInfo: function (newEmployee) {
      if (newEmployee != null && newEmployee != {}) {
        this.employee = newEmployee;
        this.employee.dateOfBirth = this.formatBindingDate(
          this.employee.dateOfBirth
        );
        this.employee.identityDate = this.formatBindingDate(
          this.employee.identityDate
        );
      }
    },
    editMode: function (newEmployee) {
      var me = this;
      if (newEmployee == enumProperties.add) {
        this.employee = {};
        // var me = this;
        axios.get(config.API_NEWEMPLOYEECODE).then(function (res) {
          var newEmployeeCode = res.data;
          //Hiển thị dialog
          me.IsShowDialog = true;
          //Gán giá trị cho ô Employee Code
          me.employee.employeeCode = newEmployeeCode;

          // 2. Focus vào ô nhập liệu đầu tiên
          me.$nextTick(function () {
            //hàm nextTick đảm bảo sau khi Render hết thì mới thực hiện Function trong hàm
            document.getElementById("txtEmpCode").focus();
          }).catch(function (res) {
            console.log(res);
          });
        });
      } else if (newEmployee == enumProperties.edit) {
        this.employee = this.employeeInfo;
        this.employee.dateOfBirth = this.formatBindingDate(
          this.employee.dateOfBirth
        );
        this.employee.identityDate = this.formatBindingDate(
          this.employee.identityDate
        );
      } else if (newEmployee == enumProperties.clone) {
        this.employee = this.employeeInfo;
        this.employee.dateOfBirth = this.formatBindingDate(
          this.employee.dateOfBirth
        );
        this.employee.identityDate = this.formatBindingDate(
          this.employee.identityDate
        );
        axios.get(config.API_NEWEMPLOYEECODE).then(function (res) {
          var newEmployeeCode = res.data;
          //Hiển thị dialog
          me.IsShowDialog = true;
          //Gán giá trị cho ô Employee Code
          me.employee.employeeCode = newEmployeeCode;

          // 2. Focus vào ô nhập liệu đầu tiên
          me.$nextTick(function () {
            //hàm nextTick đảm bảo sau khi Render hết thì mới thực hiện Function trong hàm
            document.getElementById("txtEmpCode").focus();
          }).catch(function (res) {
            console.log(res);
          });
        });
      }
    },
  },
  methods: {
    /**
     * Sau khi các input nhập xong thì gán ngược giá trị
     * @param key: key cần gán giá trị
     * @param value: Giá trị được gán
     * @author: NQLINH (01/07/2022)
     */
    dataChange(key, value) {
      try {
        this.employee[key] = value;
        this.isDataChange = true;
      } catch (ex) {
        console.log(ex);
      }
    },
    /**
     * Hàm thực hiện validate Email
     * Author: NQL (31/5/2022)
     */
    validateEmail(Email) {
      var re =
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(Email);
    },
    /**
     * Hàm thực hiện blur Email
     * Author: NQL (31/5/2022)
     */
    EmailBlur(event) {
      if (this.validateEmail(event.target.value)) {
        event.target.classList.remove("m-input-error");
      } else {
        event.target.classList.add("m-input-error");
      }
    },
    /**
     * Ẩn hiện thông báo lỗi
     * Author: NQL (31/5/2022)
     */
    showOrHideDialogNotice(isShow) {
      this.ShowNoticeDialog = isShow;
    },
    /**
     * Thực hiện cất dữ liệu sau khi ấn vào btn Cất
     * Author: NQL (31/5/2022)
     */
    btnSaveOnClick() {
      try {
        this.ValidateErrors = [];
        // 1. Lấy thông tin dối tượng nhân viên được nhập trên form
        var employee = this.employee;
        // 2. Validate dữ liệu
        var isValid = true;
        var validateErrors = [];
        if (!employee.employeeCode) {
          isValid = false;
          validateErrors.push("Mã nhân viên không được phép để trống");
        }
        if (!employee.fullName) {
          isValid = false;
          validateErrors.push("Họ và tên không được phép để trống");
        }
        if (!employee.departmentId) {
          isValid = false;
          validateErrors.push("Tên đơn vị không được phép để trống");
        }

        if (!this.validateEmail(employee.email)) {
          isValid = false;
          validateErrors.push("Email không đúng định dạng");
        }

        let inputsRequired = document.querySelectorAll("[required]");
        inputsRequired.forEach((element) => {
          if (element.value == "") {
            isValid = false;
            element.classList.add("m-input-error");
            element.setAttribute("title", "Không được phép để trống");
          } else {
            element.classList.remove("m-input-error");
            element.removeAttribute("title");
          }
        });

        // Hiển thị cảnh báo không hợp lệ
        if (!isValid) {
          this.ShowNoticeDialog = true;
          this.ValidateErrors = validateErrors;
          return;
        } else {
          var me = this;
          if (this.editMode == enumProperties.add) {
            // Khi editMode = 1 (thêm mới)
            // 3. Gọi API Cất dữ liệu
            axios
              .post(config.API_EMPLOYEES, this.employee)
              .then(function (res) {
                // hiển thị toastMsg thêm thành công
                me.messageToast = "Thêm thành công nhân viên";
                me.statusToastMsg = "m-toast-icon-success";
                // gọi hàm LoadData
                me.$emit("loadData");
                // tắt Dialog hiển thị nhân viên
                me.$emit("showHideDialogDetail", false);
                return;
              })
              .catch(function (error) {
                me.ShowNoticeDialog = true;
                me.ValidateErrors = [error.response.data.userMsg];
                return;
              });
          } else if (this.editMode == enumProperties.edit) {
            // editmode =0 : sửa
            axios
              .put(
                config.API_EMPLOYEES + this.employee.employeeId,
                this.employee
              )
              .then(function (res) {
                me.messageToast = "Sửa thành công nhân viên";
                me.statusToastMsg = "m-toast-icon-success";
                me.$emit("loadData");
                me.$emit("showHideDialogDetail", false);
                return;
              })
              .catch(function (res) {
                console.log(res);
                me.ShowNoticeDialog = true;
                me.ValidateErrors = [res.response.data.userMsg];
                return;
              });
          } else {
            axios
              .post(config.API_EMPLOYEES, this.employee)
              .then(function (res) {
                // hiển thị toastMsg thêm thành công
                me.messageToast = "Nhân bản thành công nhân viên";
                me.statusToastMsg = "m-toast-icon-success";
                // gọi hàm LoadData
                me.$emit("loadData");
                // tắt Dialog hiển thị nhân viên
                me.$emit("showHideDialogDetail", false);
                return;
              })
              .catch(function (error) {
                me.ShowNoticeDialog = true;
                me.ValidateErrors = [error.response.data.userMsg];
                return;
              });
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    getNewEployeeCode() {
      axios.get(config.API_NEWEMPLOYEECODE).then(function (res) {
        return res.data;
      });
    },
    /**
     * Thực hiện cất và thêm dữ liệu
     * Author: NQL (31/5/2022)
     */
    btnSaveAddOnClick() {
      try {
        this.ValidateErrors = [];
        // 1. Lấy thông tin dối tượng nhân viên được nhập trên form
        var employee = this.employee;
        // 2. Validate dữ liệu
        var isValid = true;
        var validateErrors = [];
        if (!employee.employeeCode) {
          isValid = false;
          validateErrors.push("Mã nhân viên không được phép để trống");
        }
        if (!employee.fullName) {
          isValid = false;
          validateErrors.push("Họ và tên không được phép để trống");
        }
        if (!employee.departmentId) {
          isValid = false;
          validateErrors.push("Tên đơn vị không được phép để trống");
        }

        if (!this.validateEmail(employee.email)) {
          isValid = false;
          validateErrors.push("Email không đúng định dạng");
        }

        let inputsRequired = document.querySelectorAll("[required]");
        inputsRequired.forEach((element) => {
          if (element.value == "") {
            isValid = false;
            element.classList.add("m-input-error");
            element.setAttribute("title", "Không được phép để trống");
          } else {
            element.classList.remove("m-input-error");
            element.removeAttribute("title");
          }
        });

        // Hiển thị cảnh báo không hợp lệ
        if (!isValid) {
          this.ShowNoticeDialog = true;
          this.ValidateErrors = validateErrors;
          return;
        } else {
          var me = this;
          if (this.editMode == enumProperties.add) {
            // Khi editMode = 1 (thêm mới)
            // 3. Gọi API Cất dữ liệu
            axios
              .post(config.API_EMPLOYEES, this.employee)
              .then(function (res) {
                me.employee = {};
                me.ShowNoticeDialog = true;
                me.ValidateErrors = ["Thêm thành công!"];
                me.$emit("loadData");
                me.$emit("showHideDialogDetail", false);
                console.log(me.employee);
                return;
              })
              .catch(function (error) {
                // me.statusNotice = "m-notification-error";
                me.ValidateErrors = [error.response.data.userMsg];
                me.ShowNoticeDialog = true;
                return;
              });
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Thực hiện đóng dialog
     * Author: NQL (31/5/2022)
     */
    btnCloseDialog() {
      debugger
      if(this.isDataChange){
        debugger
        this.showConfirmNotice = true;
      }
      else{
        this.$emit("showHideDialogDetail", false);
      }
    },
    /**
     * Hàm định dạng hiển thị năm/tháng/ngày
     * @param {Date} date
     * Authur: NQL (31/5/2022)
     */
    formatBindingDate(date) {
      try {
        if (date != null) {
          date = new Date(date);
          // Chuyển sang dữ liệu dạng Date
          let day = date.getDate();
          day = day < 10 ? `0${day}` : day;
          let month = date.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          let year = date.getFullYear();
          return `${year}-${month}-${day}`;
        } else {
          return null;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      employee: {},
      ShowNoticeDialog: false,
      ValidateErrors: [],
      ShowSuccessDialog: false,
      ValidateSuccess: [],
      departments: [],
      statusToastMsg: null,
      messageToast: null,
      isDataChange: false,
      showConfirmNotice: false,
    };
  },
};
</script>