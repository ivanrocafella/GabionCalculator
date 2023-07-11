interface AppEnv {
  production: boolean;
  apiUrl: string;
}

export const environment: AppEnv = {
  production: false,
  apiUrl: 'http://192.168.0.232/api'
}

