<template>
  <h1 class="text-h4 mt-4 mb-6">{{ title }}</h1>
</template>

<script>
export default {
  name: "PageTitle",
  props: {
    title: String,
  },
};
</script>