import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    https: {
      key: '../Certificates/React/key.pem',
      cert: '../Certificates/React/cert.pem',
    },
    host: '0.0.0.0',
    port: 443,
  },
})
