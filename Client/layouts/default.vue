<template>
  <v-app>
    <v-app-bar clipped-left app height="70" class="white">
      <v-btn icon class="ml-0" @click="GoBack">
        <v-icon size="35"> mdi-chevron-left </v-icon>
      </v-btn>
      <v-spacer />

      <nuxt-link class="mr-1 black--text" to="/">
        <v-row justify="start" align="center">
          <p v-if="user" class="ma-0 text-h5">
            {{ user.name }}
          </p>
        </v-row>
      </nuxt-link>

      <v-spacer v-if="$vuetify.breakpoint.mobile" />

      <v-btn v-if="$vuetify.breakpoint.mobile" icon class="mr-0" @click="drawer = !drawer">
        <v-icon size="35"> mdi-menu </v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer v-if="user" v-model="drawer" :mini-variant="false" :clipped="true" :app="true">
      <template v-slot:prepend>
        <v-list-item two-line>
          <v-list-item-action>
            <v-icon color="primary">mdi-account</v-icon>
          </v-list-item-action>
          <v-list-item-content v-if="user">
            <v-list-item-title>{{ user.name }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </template>
      <v-divider></v-divider>

      <v-list v-if="user">
        <v-list-item v-for="(item, i) in items" :key="i" :to="item.to" router exact>
          <v-list-item-action>
            <v-icon :color="item.color">{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>
              {{ item.title }}
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>

        <v-list-item @click="Logout">
          <v-list-item-action>
            <v-icon color="black">mdi-logout</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            Útskrá
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <error-alert class="mt-8 mb-0 mx-3" />
      <nuxt />
    </v-main>
  </v-app>
</template>

<script>
import text from "~/lang/text";
import { mapState } from "vuex";

export default {
  name: "default",
  data() {
    return {
      c: text,
      colorKey: "PrimaryColor",
      clipped: false,
      drawer: false,
      fixed: false,
      items: [
        {
          icon: "mdi-glass-mug-variant",
          title: "Bjórar",
          position: "top",
          to: "/",
          color: "primary",
        },
        {
          icon: "mdi-chart-bar",
          title: "Niðurstöður",
          position: "top",
          to: "/beer-result",
          color: "primary",
        },
      ],
    };
  },
  computed: {
    ...mapState("auth", ["loggedIn", "user"]),
  },
  created() {
  },
  watch: {
    "$vuetify.breakpoint.mobile": function (newVal) {
      if (!newVal) {
        this.drawer = true;
      }
    },
  },
  methods: {
    Logout() {
      this.$router.push("/login");
      this.$auth.logout();
    },
    GoBack() {
      if (window.history.length > 2) {
        this.$router.go(-1);
      } else {
        this.$router.push("/");
      }
    }
  },
};
</script>
