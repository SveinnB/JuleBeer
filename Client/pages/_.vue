<script>
export default {
  middleware:[
    function ({ store, redirect }) {
      if (store.state.auth.loggedIn) {
        return redirect('/');
      }
      return redirect('/login');
    },
  ]
}
</script>