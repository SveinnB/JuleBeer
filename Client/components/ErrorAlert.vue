<template>
  <v-alert
    :value="visible"
    dense
    outlined
    border="left"
    type="error"
    color="p_red"
  >
    {{ text }}
    <template v-slot:prepend>
      <v-icon color="p_red" class="mr-5"> mdi-alert-octagon </v-icon>
    </template>
    <template v-slot:close="{ toggle }">
      <v-btn icon color="p_red" @click="CloseAlert">
        <v-icon> mdi-close-circle </v-icon>
      </v-btn>
    </template>
  </v-alert>
</template>

<script>
export default {
  name: "ErrorAlert",
  computed: {
    visible: function () {
      if (this.$store.state.error) {
        console.error(this.$store.state.error);
        return true;
      } else {
        return false;
      }
    },
    text: function () {
      const error = this.$store.state.error;
      if (error && error.response) {
        if (error.response.data) {
          if (typeof error.response.data == "string") {
            return `${error.response.data}`;
          }
          return `${JSON.stringify(error.response.data)}`;
        }
        return `${error.response.status} error`;
      }
      if (error && error.message) {
        return error.message;
      }
      if (typeof error === "string") {
        return error;
      }
      return "Error";
    },
  },
  methods: {
    CloseAlert() {
      this.$store.dispatch("setError", null);
    },
  },
};
</script>
