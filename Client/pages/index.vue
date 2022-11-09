<template>
  <v-container>
    <page-title title="Bjórar" />

    <v-row justify="center">
      Hallo
    </v-row>


  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "CoverPage",
  data() {
    return {
      loading: false,
      beerList: [],
    };
  },
  async created() {
    await this.GetBeers();
  },
  computed: {
    ...mapState("auth", ["loggedIn", "user"]),
  },
  methods: {
    async GetBeers() {
      try {
        this.loading = true;
        this.beerList = await this.$axios.$get("beer/GetBeers");
        console.log(this.beerList);
        this.loading = false;
      } catch (e) {
        this.loading = false;
        this.$store.dispatch("setError", e);
      }
    },
  },
};
</script>


