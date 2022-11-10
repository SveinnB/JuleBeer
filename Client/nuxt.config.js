import colors from 'vuetify/es5/util/colors'
import pkg from './package.json';

export default {
  // Target: https://go.nuxtjs.dev/config-target
  target: 'static',

  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    title: 'Jólabjór',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'initial-scale=1.0, user-scalable=no, width=device-width' },
      { hid: 'description', name: 'description', content: 'Jólabjór' },
      { name: 'mobile-web-app-capable', content: 'yes' },
      { name: 'apple-touch-fullscreen', content: 'yes' },
      { name: 'apple-mobile-web-app-capable', content: 'yes' },
    ],
    link: [
      { rel: 'icon', type: 'image/png', href: '/BeerIcon.png' },
    ]
  },

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    '@/assets/global.css'
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    { src: '~/plugins/vue-simple-photoswipe', mode: 'client' },
    { src: '~/plugins/pwa-update.js', mode: 'client' },
  ],

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    '@nuxtjs/axios',
    '@nuxtjs/pwa',
    '@nuxtjs/auth-next',
    '@nuxtjs/dayjs'
  ],

  auth: {
    plugins: ['~/plugins/auth.js'],
    //localStorage: true,
    strategies: {
      local: {
        scheme: 'local',
        token: {
          property: 'token',
          required: true,
          type: 'Bearer'
        },
        user: {
          property: false,
          autoFetch: true
        },
        endpoints: {
          login: { url: '/auth/login', method: 'post' },
          logout: false,
          user: { url: '/auth/me', method: 'get' }
        }
      }
    },
    redirect: {
      login: '/login',
      logout: '/login',
      callback: '/login',
      home: false
    }
  },

  router: {
    middleware: ['auth', 'clearError']
  },

  axios: {
    baseURL: process.env.API_URL
  },

  // Optional
  dayjs: {
    locales: ['en'],
    defaultLocale: 'en',
    defaultTimeZone: 'Atlantic/Reykjavik',
    plugins: [
      'utc', // import 'dayjs/plugin/utc'
      'timezone' // import 'dayjs/plugin/timezone'
    ] // Your Day.js plugin
  },

  publicRuntimeConfig: {
    mobileWidth: 600,
    clientVersion: pkg.version,
  },

  //PWA module configuration: https://go.nuxtjs.dev/pwa
  pwa: {
    meta: {
      name: 'JólaBjór',
      author: 'Dr. Svennsen',
      mobileAppIOS: true,
      appleStatusBarStyle: 'default',
      nativeUI: true,
      favicon: false,
      theme_color: '#B71C1C'
    },
    icon: {
      fileName: 'BeerIcon.png',
      iosSizes: []
    },
    manifest: {
      lang: 'en',
      name: 'JólaBjór',
      short_name: 'JólaBjór',
      description: 'JólaBjór',
      background_color: '#B71C1C'
    }
  },

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    //customVariables: ['~/assets/variables.scss'],
    theme: {
      themes: {
        light: {
          primary: '#B71C1C',
          p_blue: colors.blue.darken4,
          p_red: colors.red.darken4,
          p_green: colors.green.darken3,
          p_orange: colors.orange.darken3,
          p_black: colors.black,
          p_purple: colors.purple.darken2
        }
      }
    },
  },

  generate: {
    fallback: true
  }
}
