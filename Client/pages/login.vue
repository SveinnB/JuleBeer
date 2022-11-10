<template>
  <v-container fluid fill-height>

    <v-row class="ma-0" justify="center">
      <p class="ma-0 text-center text-h4">Jólabjór Heilbrigðislausna 2022</p>
    </v-row>

    <v-row justify="center">
      <v-card class="mx-6 mt-3" :width="700" :elevation="8">
        <v-card-title>
          Innskráning
        </v-card-title>

        <v-card-text>
          <v-text-field v-model="name" label="Nafn" required>
          </v-text-field>
          <v-row justify="end" class="ma-0">
            <v-btn color="primary" class="ma-0" :loading="loginLoading" outlined @click="Login">
              Innskrá
            </v-btn>
          </v-row>
        </v-card-text>
      </v-card>
    </v-row>



    <v-row justify="center">
      <v-img src="/BeerIcon.png" contain max-height="250" max-width="250">
      </v-img>
    </v-row>

  </v-container>
</template>


<script>
export default {
  name: "LoginPage",
  layout: "login",
  data() {
    return {
      name: null,
      loginLoading: false,
      alert: false,
      alertMessage: null,
    };
  },
  methods: {
    async Login() {
      try {
        this.loginLoading = true;
        const loginData = {
          name: this.name,
        };
        var res = await this.$auth.loginWith("local", {
          data: loginData,
        });
        if (res && res.status == 200) {
          this.$router.push("/");
        }
        this.loginLoading = false;
      } catch (err) {
        this.loginLoading = false;
        if (err && err.response && err.response.data) {
          this.alertMessage = err.response.data;
          this.alert = true;
        }
      }
    }
  },
};
</script>

<style scoped>
div.absolute {
  position: absolute;
  right: 15px;
  bottom: 10px;
}
</style>
