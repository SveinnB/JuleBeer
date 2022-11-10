<template>
  <v-container>
    <h1 class="text-h4 mt-4 mb-2">Niðurstöður</h1>

    <p class="ma-0 text-subtitle-1">Meðaltal</p>
    <p class="ma-0 text-subtitle-1">Fjöldi þátttakenda: {{ numberOfUsers }}</p>
    <p class="text-subtitle-1">Fjöldi einkunnagjafa: {{ numberOfRatings }}</p>

    <v-data-table class="row-pointer" :loading="loading" :disable-filtering="true" :disable-sort="true"
      :hide-default-footer="true" :mobile-breakpoint="0" :headers="headers" :items="beerList" @click:row="ClickOnRow">

      <template v-slot:loading>
        Sækja
      </template>

      <template v-slot:no-data>
        No beers
      </template>

      <template v-slot:item.imageUrl="{ item }">
        <v-img :src="item.imageUrl" :height="100" :width="50" contain class="my-3"></v-img>
      </template>
    </v-data-table>

  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "BeerResultPage",
  data() {
    return {
      loading: false,
      numberOfUsers: 0,
      numberOfRatings: 0,
      beerList: [],
      headers: [
        {
          text: "Mynd",
          align: "start",
          value: "imageUrl",
        },
        {
          text: "Nafn",
          align: "start",
          value: "name",
        },
        {
          text: "Stjörnur - meðaltal",
          align: "center",
          value: "averageStars",
        },
      ]
    };
  },
  async created() {
    await this.GetBeerResult();
  },
  computed: {
    ...mapState("auth", ["loggedIn", "user"]),
  },
  methods: {
    async GetBeerResult() {
      try {
        this.loading = true;
        var result = await this.$axios.$get("beer/GetBeerResult");
        this.beerList = result.beerResults;
        this.numberOfRatings = result.numberOfRatings;
        this.numberOfUsers = result.numberOfUsers;
        this.loading = false;
      } catch (e) {
        this.loading = false;
        this.$store.dispatch("setError", e);
      }
    },
    ClickOnRow(item) {
      this.$router.push(`/beers/${item.id}`);
    },
  },
};
</script>

<style lang="css" scoped>
.row-pointer>>>tbody tr :hover {
  cursor: pointer;
}
</style>



