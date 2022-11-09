export default function({ $auth }) {
  $auth.onError((error, name) => {
    console.error(name, error)
  })
}
