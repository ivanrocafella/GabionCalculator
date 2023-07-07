interface AppEnv {
  production: boolean;
  apiUrl: string;
}

export const environment: AppEnv = {
  production: false,
  apiUrl: 'https://localhost:5001'
}

