import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    https: {
      key: 'Certs/key.pem',
      cert: 'Certs/cert.pem',
    },
    //host: '0.0.0.0',
    //port: 443,
  },
})
