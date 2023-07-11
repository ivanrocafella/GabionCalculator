interface AppEnv {
  production: boolean;
  apiUrl: string;
}

export const environment: AppEnv = {
  production: false,
  apiUrl: 'http://localhost:5001'
}

