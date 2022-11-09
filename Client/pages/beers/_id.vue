<template>
    <v-container>
        <h1 class="text-h4 text-center mt-4 mb-8">{{ beer.name }}</h1>

        <v-row justyfy="center" class="mb-5">
            <v-img :max-height="200" :src="beer.imageUrl" contain></v-img>
        </v-row>

        <p class="text-h4">
            Lýsing
        </p>
        <p class="text-subtitle-1">
            {{ beer.description }}
        </p>

        <p class="text-h4 mb-8">
            ABV - {{ beer.abv }} %
        </p>

        <v-row justify="center">
            <v-rating background-color="red lighten-2" empty-icon="mdi-star-outline" full-icon="mdi-star"
                half-icon="mdi-star-half-full" length="10" size="20" :value="beer.myStars" @input="RatingChange">
            </v-rating>
        </v-row>

    </v-container>
</template>
  
<script>
import { mapState } from "vuex";

export default {
    name: "BeerId",
    data() {
        return {
            loading: false,
        };
    },
    async asyncData({ params, $axios, error }) {
        try {
            const beer = await $axios.$get(`/Beer/GetBeerById?id=${params.id}`);
            return { beer };
        } catch (e) {
            error(e);
        }
    },
    computed: {
        ...mapState("auth", ["loggedIn", "user"]),
    },
    methods: {
        async RatingChange(item) {
            try {
                const body = {
                    beerId: this.beer.id,
                    stars: item
                };
                await this.$axios.$post("beer/GiveStars", body);
            } catch (e) {
                this.loading = false;
                this.$store.dispatch("setError", e);
            }
        },
        async GetBeers() {

        },
    },
};
</script>
  
<style lang="css" scoped>
.row-pointer>>>tbody tr :hover {
    cursor: pointer;
}
</style>
