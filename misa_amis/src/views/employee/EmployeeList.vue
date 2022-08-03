<template>
  <div
    v-if="isShowExpand"
    :style="{ top: expandTop, left: expandLeft }"
    class="m-expand"
  >
    <div class="text-align-left m-expand-item" @click="btnReplication">
      Nhân bản
    </div>
    <div class="text-align-left m-expand-item" @click="btnDelete">Xóa</div>
    <div class="text-align-left m-expand-item">Ngừng sử dụng</div>
  </div>
  <div class="m-content">
    <div class="m-page-header">
      <div class="m-page-tile">
        <div class="title m-title-list">Nhân viên</div>
      </div>
      <div class="m-page-button">
        <button id="btnAddEmp" class="m-button" @click="btnAddOnClick">
          Thêm mới nhân viên
        </button>
      </div>
    </div>

    <div class="m-wrap-content">
      <div class="m-page-toolbar">
        <div class="multiDelete">
          <div class="multiDelete_wrap">
            <button class="btn_multiDelete" @click="btnOptionsOnClick">
              <span>Thực hiện xóa hàng loạt</span>
              <div class="mi-16 m-icon-multi"></div>
            </button>
            <div v-if="isShowOptions" @click="blurToggle" class="item-list">
              <div class="item" @click="btnDeletes">Xóa</div>
            </div>
          </div>
        </div>
        <div class="m-button-container">
          <input
            type="text"
            id="search"
            class="m-input m-input-search"
            placeholder="Tìm kiếm theo mã, tên nhân viên"
            style="width: 240px; height: 32px"
            @blur="blurInputSearch($event.target.value)"
          />
          <div class="m-icon m-icon-search mi-16 m-search-input-icon"></div>
          <div class="m-btn-refresh">
            <button
              id="btnRefresh"
              class="m-icon m-icon-refresh mi-24"
              @click="btnRefreshOnClick"
            ></button>
          </div>
          <div class="m-btn-export">
            <button
              id="btnExport"
              class="m-icon m-icon-export mi-24"
              @click="btnExportExcelOnClick"
            ></button>
          </div>
        </div>
      </div>

      <div class="m-page-grid">
        <div class="m-grid">
          <table id="tblEmployeeList" class="m-table">
            <thead>
              <tr>
                <th class="m-th-checkAll">
                  <input
                    type="checkbox"
                    name="checkbox"
                    @input="checkAll($event)"
                    :checked="isCheckAll"
                    class="checkAllItem"
                  />
                </th>
                <th
                  class="text-align-left m-col-id"
                  style="min-width: 150px; width: 150px"
                >
                  Mã nhân viên
                </th>
                <th class="text-align-left m-col-name" style="min-width: 250px">
                  Tên nhân viên
                </th>
                <th
                  class="text-align-left m-col-gender"
                  style="min-width: 100px"
                >
                  Giới tính
                </th>
                <th class="text-align-left m-col-dob" style="min-width: 100px">
                  Ngày sinh
                </th>
                <th
                  class="text-align-left m-col-identityNum"
                  style="min-width: 150px"
                >
                  Số CMND
                </th>
                <th
                  class="text-align-left m-col-position"
                  style="min-width: 150px"
                >
                  Chức danh
                </th>
                <th
                  class="text-align-left m-col-department"
                  style="min-width: 200px"
                >
                  Tên đơn vị
                </th>
                <th
                  class="text-align-left m-col-accountNum"
                  style="min-width: 150px"
                >
                  Số tài khoản
                </th>
                <th
                  class="text-align-left m-col-bankName"
                  style="min-width: 200px"
                >
                  Tên ngân hàng
                </th>
                <th
                  class="text-align-left m-col-branchBank"
                  style="min-width: 200px"
                >
                  Chi nhánh tk ngân hàng
                </th>
                <th
                  class="text-align-left m-col-feature"
                  style="min-width: 120px"
                >
                  Chức năng
                </th>
              </tr>
            </thead>

            <tbody v-if="totalRecord > 0">
              <tr
                v-for="employee in employees"
                :key="employee.employeeId"
                @dblclick="rowOnDblClick(employee)"
              >
                <td class="m-check">
                  <input
                    type="checkbox"
                    name="checkbox"
                    class="checkboxItem"
                    :value="employee.employeeId"
                    @input="updateCheckboxs($event.target)"
                    :checked="
                      checkboxs
                        ? checkboxs.includes(employee.EmployeeId)
                        : false
                    "
                  />
                </td>
                <td class="text-align-left">
                  {{ employee.employeeCode }}
                </td>
                <td class="text-align-left">
                  {{ employee.fullName }}
                </td>
                <td class="text-align-left">
                  {{ employee.genderName }}
                </td>
                <td class="text-align-center">
                  {{ formatDate(employee.dateOfBirth) }}
                </td>
                <td class="text-align-left">
                  {{ employee.identityNumber }}
                </td>
                <td class="text-align-left">
                  {{ employee.positionName }}
                </td>
                <td class="text-align-left">
                  {{ employee.departmentName }}
                </td>
                <td class="text-align-left">
                  {{ employee.bankAccountNumber }}
                </td>
                <td class="text-align-left">
                  {{ employee.bankName }}
                </td>
                <td class="text-align-left">
                  {{ employee.bankBranchName }}
                </td>
                <td class="text-align-center m-col-feature">
                  <div class="m-btn-feature">
                    <button class="m-btn-edit" @click="rowOnDblClick(employee)">
                      Sửa
                    </button>
                    <button
                      class="m-btn-dropdown m-icon m-icon-arrow-up-blue mi-16"
                      @click="
                        btnShowExpand(
                          $event,
                          employee
                          // employee.employeeId,
                          // employee.employeeCode
                        )
                      "
                    ></button>
                  </div>
                </td>
              </tr>
            </tbody>
            <div v-else-if="totalRecord == null"><PageLoading /></div>
          </table>

          <div class="m-paging">
            <div class="m-paging-left">
              <div class="m-total-record">
                Tổng số: <b>{{ totalRecord }}</b> bản ghi
              </div>
            </div>
            <!-- <div class="m-paging-center"></div> -->
            <div class="m-paging-right">
              <div class="m-record-per-page">
                <select
                  name=""
                  class="m-number-per-page"
                  @change="cbxPagingChange($event)"
                >
                  <option :value="totalRecord">Tất cả bản ghi</option>
                  <option :value="10">10 bản ghi trên 1 trang</option>
                  <option :value="20">20 bản ghi trên 1 trang</option>
                  <option :value="30">30 bản ghi trên 1 trang</option>
                  <option :value="40">40 bản ghi trên 1 trang</option>
                </select>
              </div>

              <paginate
                :page-count="totalPage"
                :click-handler="pagination"
                :prev-text="'Trước'"
                :next-text="'Sau'"
                :container-class="'m-page-number'"
              >
              </paginate>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- @ để bên Detail gọi dùng $emit -->
  <EmployeeDetail
    :IsShowDialogChild="IsShowDialog"
    :employeeInfo="employeeInfo"
    :editMode="editMode"
    @showHideDialogDetail="showDialogDetail"
    @loadData="btnRefreshOnClick"
  />

  <div v-if="isShowExpand" class="outside" @click="expandOnBlur"></div>

  <!-- Xác nhận xóa -->
  <ConfirmNotice
    :ShowConfirmNotice="showConfirmNotice"
    :EmployeeId="employeeIdDelete"
    :EmployeeCode="employeeCodeDelete"
    @callParent="showOrHideConfirmNotice"
    @loadData="btnRefreshOnClick"
  />
  <!-- Thông báo lỗi -->
  <DialogNotice
    :ShowNoticeDialog="ShowNoticeDialog"
    :Msgs="ValidateErrors"
    @callParent="showOrHideDialogNotice"
  />
</template>

<style scoped>
@import url("../../styles/page/employee.css");
</style>

<script>
import axios from "axios";
import EmployeeDetail from "./EmployeeDetail.vue";
import config from "../../../config.js";
import enumProperties from "../../../enumProperties.js";

export default {
  name: "EmployeeList",
  components: {
    EmployeeDetail,
  },
  created() {
    /**
     * Thực hiện lấy dữ liệu ở giai đoạn created
     * Author: NQL (31/5/2022)
     */
    try {
      var me = this;
      axios.get(config.API_EMPLOYEES).then((res) => {
        me.employees = res.data;
        me.totalRecord = res.data.length;
        me.pageSize = res.data.length;
        // me.pagination(me.pageSize, me.pageNumber);
        console.log(me.employees);
      });
    } catch (error) {
      console.log(error);
    }
  },
  methods: {
    /**
     * Thực hiện xóa hàng loạt
     * Author: NQLINH(31/5/2022)
     */
    btnDeletes(){
      debugger
      var ids = this.checkboxs.join();
      console.log(ids);
        axios
          .delete(config.API_EMPLOYEES/`${ids}`)
          .then((res) => {
            console.log(res);
            console.log("xóa okeee");
            
          })
          .catch((error) => {
            
          });
        
        this.isShowOptions = false;
    },
    updateCheckboxs(value, check){
       if (check) {
          this.checkboxs.push(value);
        } else {
          this.isCheckAll = false;
          var index = this.checkboxs.indexOf(value);
          if (index !== -1) {
            this.checkboxs.splice(index, 1);
          }
        }
        if (this.checkboxs.length == this.employees.length) {
          this.isCheckAll = true;
        } else {
          this.isCheckAll = false;
        }
    },
    /**
     * check ID được chọ rồi map lại với nhau
     * Author: NQLINH (31/5/2022)
     */
    checkAll(event) {
      this.checkboxs = [];
      if (event.target.checked) {
        this.checkboxs = this.employees.map((employee) => employee.EmployeeId);
      }
      this.isCheckAll = !this.isCheckAll;
    },
    /**
     * Hàm thực hiện blur tắt xóa Nhiều
     * Author: NQLINH (18/06/2022)
     */
    blurToggle() {
      this.isEnabled = false;
    },

    /**
     * hàm thực hiện mở xóa nhiều
     * Author: NQLINH (18/06/2022)
     */
    btnOptionsOnClick() {
      this.isShowOptions = !this.isShowOptions;
    },

    /**
     * Nhân bản nhân viên
     * @param employeeId: id nhân viên cần nhân bản
     * @author: NQLINH (28/06/2022)
     */
    async btnReplication() {
      this.IsShowDialog = true;
      this.isShowExpand = false;
      this.editMode = enumProperties.clone;
    },
    /**
     * Hàm xuất ra excel
     * Author: NQL (31/5/2022)
     */
    btnExportExcelOnClick() {
      try {
        window.open(config.API_EXPORT_EXCEL, "Download");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Ẩn hiện thông báo
     * Author: NQL (31/5/2022)
     */
    showOrHideDialogNotice(isShow) {
      this.ShowNoticeDialog = isShow;
    },
    /**
     * Thực hiện hiển thị form chi tiết thông tin nhân viên
     * Author: NQL (31/5/2022)
     */
    async btnAddOnClick() {
      try {
        this.editMode = 1;
        this.employeeInfo = null;
        this.IsShowDialog = true;
        // 1. Lấy mã nhân viên mới, binding lên form chi tiết
        let newEmployeeCode = "";
        await axios
          .get(config.API_NEWEMPLOYEECODE)
          .then(function (res) {
            newEmployeeCode = res.data;
          })
          .catch(function (res) {
            console.log(res);
          });

        //Hiển thị dialog
        this.IsShowDialog = true;
        //Gán giá trị cho ô Employee Code
        this.employee.employeeCode = newEmployeeCode;

        // 2. Focus vào ô nhập liệu đầu tiên
        this.$nextTick(function () {
          //hàm nextTick đảm bảo sau khi Render hết thì mới thực hiện Function trong hàm
          document.getElementById("txtEmpCode").focus();
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Hàm thực hiện nút expand
     * AUTHOR: NQL (01/06/2022)
     */
    btnShowExpand(event, employee) {
      let top = event.clientY + 15;
      let left = event.clientX - 100;
      this.isShowExpand = true;
      this.expandTop = top + "px";
      this.expandLeft = left + "px";
      this.employeeIdDelete = employee.employeeId;
      this.employeeCodeDelete = employee.employeeCode;
      this.employeeInfo = employee;
    },
    /**
     * Hàm thực hiện xóa dữ liệu
     * Author: NQL (31/5/2022)
     */
    btnDelete() {
      this.showConfirmNotice = true;
      this.isShowExpand = false;
    },
    /**
     * hàm set giá trị của ShowConfirmDialog
     * Author: NQL (31/5/2022)
     */
    showOrHideConfirmNotice(isShow) {
      this.showConfirmNotice = isShow;
    },
    /**
     * Hàm thực hiện kích đúp vào row thì hiển thị thông tin nhân viên
     * Author: NQL (31/5/2022)
     */
    rowOnDblClick(employee) {
      this.editMode = 0; //Danh dau trang thai sua
      this.employeeInfo = employee;
      this.IsShowDialog = true;
    },
    /**
     * Hàm thực hiện hiển thị dialog
     * Author: NQL (31/5/2022)
     */
    showDialogDetail(isShow) {
      this.IsShowDialog = isShow;
    },

    /**
     * hàm thực hiện load lại dữ liệu
     * @author: NQL (31/5/2022))
     */
    btnRefreshOnClick() {
      try {
        var me = this;
        me.totalRecord = null;
        me.employees = [];
        document.getElementById("search").value = "";
        axios.get(config.API_EMPLOYEES).then((res) => {
          me.employees = res.data;
          me.totalRecord = res.data.length;
        });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm định dạng ngày tháng
     * Author: NQL (31/5/2022)
     */
    formatDate(date) {
      try {
        if (date != null) {
          date = new Date(date);
          // Chuyển sang dữ liệu dạng Date
          let day = date.getDate();
          day = day < 10 ? `0${day}` : day;
          let month = date.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          let year = date.getFullYear();
          return `${day}/${month}/${year}`;
        } else {
          return "";
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xử lý khi click ngoài expand để ẩn expand đi
     * @author: NQL (31/5/2022))
     */
    expandOnBlur() {
      this.isShowExpand = false;
    },
    /**
     * Hàm tìm kiếm nhân viên theo keyword
     * @author: NQL (31/5/2022)
     */
    searchEmployeeByKeyWord(
      pageSize = "20",
      pageNumber = "1",
      employeeFilter = ""
    ) {
      this.employees = [];
      this.totalRecord = null;
      var me = this;
      if (employeeFilter) {
        employeeFilter = `employeeFilter=${employeeFilter}&`;
      }
      console.log(
        config.API_FILTER +
          `${employeeFilter}&page=${pageSize}&pageNumber=${pageNumber}`
      );
      axios
        .get(
          config.API_FILTER +
            `${employeeFilter}&pageSize=${pageSize}&pageNumber=${pageNumber}`
        )
        .then((res) => {
          if (res.status == 204) {
            this.ShowNoticeDialog = true;
          } else {
            me.totalRecord = res.data.totalRecord;
            me.totalPage = res.data.totalPage;
            me.employees = res.data.data;
            me.pageNumber = pageNumber;
          }
        })
        .catch((error) => {
          me.totalRecord = me.employees.length;
          console.log(error);
        });
    },
    /**
     *Hàm nhận sự kiện tìm kiếm theo từ khóa
     @author: NQL (31/5/2022)
     */
    blurInputSearch(keyWord) {
      this.txtSearch = keyWord;
      this.searchEmployeeByKeyWord(
        this.pageSize,
        this.pageNumber,
        this.txtSearch
      );
    },
    /**
     * Hàm thực hiện phân trang phân trang
     * Author: NQL (31/5/2022)
     */
    pagination(pageNumber) {
      try {
        var api = `https://localhost:7094/api/v1/Employees/filter?pageSize=${this.pageSize}&pageNumber=${pageNumber}`;
        if (this.txtSearch) {
          api =
            config.API_FILTER +
            `employeeFilter=${this.txtSearch}&pageSize=${this.pageSize}&pageNumber=${pageNumber}`;
        }

        this.employees = [];
        this.pageNumber = pageNumber;
        let me = this;
        axios
          .get(api)
          .then((res) => {
            me.totalPage = res.data.totalPage;
            me.employees = res.data.data;
            me.pageNumber = pageNumber;
          })
          .catch((error) => {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Nhận thay đổi khi combobox phân trang thay đổi
     * @author: NQL (31/05/2022)
     */
    cbxPagingChange(event) {
      this.pageSize = event.target.value;
      this.pagination(this.pageNumber);
      // if(this.txtSearch){
      //   this.pageSize = event.target.value;
      //   this.pagination(this.pageNumber);
      // } else{
      //   this.searchEmployeeByKeyWord(this.pageSize, this.pageNumber, this.txtSearch);
      // }
    },
  },
  data() {
    return {
      expandTop: 0,
      expandLeft: 0,
      editMode: 0,
      employee: {},
      employees: [],
      IsShowDialog: false,
      employeeInfo: null,
      isShowExpand: false,
      employeeIdDelete: "",
      employeeCodeDelete: "",
      showConfirmNotice: false,
      totalRecord: null,
      totalPage: null,
      pageSize: null,
      pageNumber: "1",
      ValidateErrors: [],
      ShowNoticeDialog: false,
      txtSearch: "",
      isItemListVisible: false,
      isEnabled: false,
      isShowOptions: false,
      isCheckAll: false,
      checkboxs: [],
    };
  },
};
</script>