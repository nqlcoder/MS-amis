<template>
    <div>
        <TheHeader/>
        <TheNavbar/>
        <TheContent/>
    </div>
</template>


<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheNavbar from './components/layout/TheNavbar.vue'
import TheContent from './components/layout/TheContent.vue'

export default {
  name: "App",
  components: {
      TheHeader,
      TheNavbar,
      TheContent
  },
};
</script>

<style>
@import url("./styles/main.css");

*{
    box-sizing: border-box;
    max-width: 100%;
    padding: 0;
    margin: 0;
}

body{
    font-family: MISA Fonts;
    color: #454545;
    font-size: 13px;
    margin: 0;
    height: 100%;
    overflow: hidden
}
</style>
