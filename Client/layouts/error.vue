<template>
  <v-container fill-height>
    <v-col>
      <v-row justify="center">
        <h1 v-if="error.statusCode === 401">
          {{ c.tokenExpired[$store.state.lang] }}
        </h1>
        <h1 v-else-if="error.statusCode === 404">
          {{ c.pageNotFound[$store.state.lang] }} 🧐
        </h1>
        <h1 v-else>Error 😭</h1>
      </v-row>

      <v-row justify="center">
        <p v-if="error.message">{{ error.message }}</p>
      </v-row>

      <v-row justify="center">
        <v-btn nuxt to="/" outlined color="primary">
          {{ c.home[$store.state.lang] }}
        </v-btn>
      </v-row>
    </v-col>
  </v-container>
</template>

<script>
import text from "~/lang/text";

export default {
  props: {
    error: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      c: text,
    };
  },
  watch: {
    error(val) {
      if (val && error.statusCode === 401) {
        this.$auth.logout();
        this.$router.push("/login");
      }
    },
  },
};
</script>