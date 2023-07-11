interface AppEnv {
  production: boolean;
  apiUrl: string;
}

export const environment: AppEnv = {
  production: true,
  apiUrl: 'http://192.168.0.232'
}
