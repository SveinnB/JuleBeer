<template>
  <v-container fluid fill-height>
    <v-row justify="center" align="center">
      <v-sheet class="mx-6" color="transparent" :height="60" width="100%">
        <v-alert class="ma-auto" v-model="alert" type="error" color="p_red" transition="scale-transition" outlined
          :max-width="700">
          {{ alertMessage }}
        </v-alert>
      </v-sheet>

      <v-card class="mx-6 mt-3" :width="700" :elevation="8">
        <v-card-title>
          Innskráning
        </v-card-title>

        <v-card-text>
          <v-form ref="loginForm">
            <v-text-field v-model="name" label="Nafn" required>
            </v-text-field>

            <v-row justify="end" class="ma-0">
              <v-btn color="primary" class="ma-0" :loading="loginLoading" outlined @click="Login">
                Innskrá
              </v-btn>
            </v-row>
          </v-form>
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
