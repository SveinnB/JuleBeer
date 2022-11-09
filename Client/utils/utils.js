import text from '~/lang/text'

export function GetUserFirstNameById(store, id){
  const user = store.state.users.find(x => x.id === id);
  if(user){
    return user.firstname;
  }
  else{
    return text.noDataEmployee[store.state.lang];
  }
}

export function GetDateString(date){
  const d = new Date(date);
  const dateString = `${PadLeft(d.getDate())}.${PadLeft(d.getMonth()+1)}.${d.getFullYear()}`;
  const timeString = `${PadLeft(d.getHours())}:${PadLeft(d.getMinutes())}`;
  return dateString + ' - ' + timeString;
}

export function GetDateOnly(date){
  const d = new Date(date);
  const dateString = `${PadLeft(d.getDate())}.${PadLeft(d.getMonth()+1)}.${d.getFullYear()}`;
  return dateString;
}

export function PadLeft(number){
  if(number < 10){
    return `0${number}`;
  }
  else{
    return `${number}`;
  }
}

export function RoundToOneDecimal(num){
 return Math.round((num) * 10) / 10;
}

export function NumberToString(num){
  return num.toLocaleString('de-DE');
}

export const ToBase64 = file => new Promise((resolve, reject) => {
  const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

export const LoadImage = (url) => new Promise((resolve, reject) => {
  const img = new Image();
  img.addEventListener('load', () => resolve(img));
  img.addEventListener('error', (err) => reject(err));
  img.src = url;
});

export const imageTypes = [
  "image/jpeg",
  "image/png",
  "image/apng",
  "image/bmp",
  "image/gif",
  "image/webp"
];

export const LANG_KEY = "PRIOR_LANGUAGE";
export const USE_CERTIFICATE_KEY = "PRIOR_AUTH_USE_CERTIFICATE";
export const MY_EMAIL_KEY = "PRIOR_MY_EMAIL";

export const languages = [
  {
    id: "is",
    name: "Íslenska",
    icon: "is.svg",
  },
  {
    id: "en",
    name: "English",
    icon: "gb.svg",
  }
]

export function coerceToArrayBuffer(thing, name) {
  if (typeof thing === "string") {
      // base64url to base64
      thing = thing.replace(/-/g, "+").replace(/_/g, "/");

      // base64 to Uint8Array
      var str = window.atob(thing);
      var bytes = new Uint8Array(str.length);
      for (var i = 0; i < str.length; i++) {
          bytes[i] = str.charCodeAt(i);
      }
      thing = bytes;
  }

  // Array to Uint8Array
  if (Array.isArray(thing)) {
      thing = new Uint8Array(thing);
  }

  // Uint8Array to ArrayBuffer
  if (thing instanceof Uint8Array) {
      thing = thing.buffer;
  }

  // error if none of the above worked
  if (!(thing instanceof ArrayBuffer)) {
      throw new TypeError("could not coerce '" + name + "' to ArrayBuffer");
  }

  return thing;
};

export function coerceToBase64Url(thing) {
  // Array or ArrayBuffer to Uint8Array
  if (Array.isArray(thing)) {
      thing = Uint8Array.from(thing);
  }

  if (thing instanceof ArrayBuffer) {
      thing = new Uint8Array(thing);
  }

  // Uint8Array to base64
  if (thing instanceof Uint8Array) {
      var str = "";
      var len = thing.byteLength;

      for (var i = 0; i < len; i++) {
          str += String.fromCharCode(thing[i]);
      }
      thing = window.btoa(str);
  }

  if (typeof thing !== "string") {
      throw new Error("could not coerce to string");
  }

  // base64 to base64url
  // NOTE: "=" at the end of challenge is optional, strip it off here
  thing = thing.replace(/\+/g, "-").replace(/\//g, "_").replace(/=*$/g, "");

  return thing;
};

export function IsFidoSupport(){
  if (window.PublicKeyCredential != undefined && typeof window.PublicKeyCredential == "function") {
    return true;
  }
  return false;
}

export function FormatAssertionOptions(options){
  // Turn the challenge back into the accepted format of padded base64
  options.challenge = coerceToArrayBuffer(options.challenge);

  options.allowCredentials = options.allowCredentials
    .map((c) => {
      c.id = coerceToArrayBuffer(c.id);
      return c;
    });

  return options;
}

export function FormatCredentialOptions(options){
  // Turn the challenge back into the accepted format of padded base64
  options.challenge = coerceToArrayBuffer(options.challenge);

  // Turn ID into a UInt8Array Buffer for some reason
  options.user.id = coerceToArrayBuffer(options.user.id);

  options.excludeCredentials = options.excludeCredentials.map((c) => {
    c.id = coerceToArrayBuffer(c.id);
    return c;
  });

  if(options.authenticatorSelection.authenticatorAttachment === null) {
    options.authenticatorSelection.authenticatorAttachment = undefined;
  }

  return options;
}

export function CreateFidoLoginRequest(assertedCredential, assertionOptionsId){
  let authData = new Uint8Array(assertedCredential.response.authenticatorData);
  let clientDataJSON = new Uint8Array(assertedCredential.response.clientDataJSON);
  let rawId = new Uint8Array(assertedCredential.rawId);
  let sig = new Uint8Array(assertedCredential.response.signature);
  const data = {
    id: assertedCredential.id,
    rawId: coerceToBase64Url(rawId),
    type: assertedCredential.type,
    extensions: assertedCredential.getClientExtensionResults(),
    response: {
      authenticatorData: coerceToBase64Url(authData),
      clientDataJson: coerceToBase64Url(clientDataJSON),
      signature: coerceToBase64Url(sig)
    }
  };

  return {
    id: assertionOptionsId,
    assertionRawResponse: data,
  };
}

export function CreateSaveNewCredentialRequest(newCredential, credentialOptionsId ){
  let attestationObject = new Uint8Array(newCredential.response.attestationObject);
  let clientDataJSON = new Uint8Array(newCredential.response.clientDataJSON);
  let rawId = new Uint8Array(newCredential.rawId);

  const data = {
    id: newCredential.id,
    rawId: coerceToBase64Url(rawId),
    type: newCredential.type,
    extensions: newCredential.getClientExtensionResults(),
    response: {
      attestationObject: coerceToBase64Url(attestationObject),
      clientDataJson: coerceToBase64Url(clientDataJSON),
    },
  };

  return {
    id: credentialOptionsId,
    attestationResponse: data
  };
}
