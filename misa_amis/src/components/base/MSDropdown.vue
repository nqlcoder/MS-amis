<template>
  <div class="m-dropdown">
    <input
      type="text"
      class="m-input m-dropdown-input"
      required
      :value="value"
    />
    <button class="m-dropdown-button" @click="btnShowDropdownOnClick">
      <div class="m-dropdown-button-icon m-icon m-icon-dropdown"></div>
    </button>
    <div v-if="isShowDropdown" class="m-dropdown-data">
      <div
        v-for="department in departments"
        :key="department.DepartmentId"
        class="m-dropdown-item"
        @click="
          selectDropdownOnClick(
            department.DepartmentName,
            department.DepartmentId
          )
        "
        :class="checkSelected(value, department.DepartmentName)"
        :value="department.DepartmentId"
      >
        {{ department.DepartmentName }}
      </div>
    </div>
  </div>
</template>

<style scoped>
@import url("../../style/base/dropdown.css");
</style>

<script>
import axios from "axios";
import { reactive, toRefs, ref } from "vue";

export default {
  props: ["api", "dropdownData", "employee.DepartmentId"],
  setup(props, { emit }) {
    var isShowDropdown = ref(false);
    const state = reactive({
      departments: [],
      data: null,
      value: null,
    });

    /**
     * Hàm fetch data theo api
     * @AUTHOR: VTSON
     */
    function fetchApi(api) {
      state.data = props.dropdownData;
      axios
        .get(api)
        .then((res) => {
          state.departments = res.data;
          if (state.data) {
            for (const department of res.data) {
              if (
                getValueByData(
                  department.DepartmentId,
                  department.DepartmentName
                )
              ) {
                state.value = getValueByData(
                  department.DepartmentId,
                  department.DepartmentName
                );
              }
            }
          } else {
            // state.value = res.data[0].DepartmentName
            // state.data = res.data[0].DepartmentId
          }
        })
        .catch((error) => {
          console.log(error);
        });
    }

    /**
     * Hàm thực hiện show dropdown
     * @AUTHOR: VTSON (03/06/2022)
     */
    function btnShowDropdownOnClick() {
      isShowDropdown.value = !isShowDropdown.value;
    }

    /**
     * Hàm sử lý sự kiện click vào item
     * @AUTHOR: VTSON
     */
    function selectDropdownOnClick(value, data) {
      state.value = value;
      state.data = data;
      emit("updateDepartmentId", data);
      isShowDropdown.value = false;
    }

    /**
     * Hàm kiểm tra item được chọn.
     * @AUTHOR: VTSON
     */
    function checkSelected(currentValue, value) {
      if (currentValue == value) return "m-dropdown-item-selected";
    }

    /**
     * Hàm lấy giá trị theo id
     * @AUTHOR: VTSON
     */
    function getValueByData(data, value) {
      if (state.data == data) {
        return value;
      }
    }

    fetchApi(props.api);

    return {
      ...toRefs(state),
      btnShowDropdownOnClick,
      selectDropdownOnClick,
      checkSelected,
      isShowDropdown,
      getValueByData,
    };
  },
};
</script>
