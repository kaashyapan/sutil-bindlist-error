import { defineConfig } from "vite";

export default defineConfig({
  build: {
    lib: 
      {
        entry: {
          index: "./index.html", 
          login: "./login.html",
        },
        formats: ["es"]
      },
  },
  plugins: [], 
  server: {
    host: '0.0.0.0',
    port: 4000,
    /*
    headers: {
      'Cache-Control': "no-cache, no-store, must-revalidate",
      'Expires': 0
    },
    */
    watch: {
      ignored: [
        "**/node_modules/**",
        "**/fable_modules/**",
        "**/bin/**",
        "**/obj/**",
        "**/.fable/**",
        "**/*.fs"
      ],
    },
    proxy: {
      "/api": {
        target: "http://52.76.217.181:8080",
        changeOrigin: true,
        secure: false,
        ws: false,
      },
      "/signalr": {
        target: "http://52.76.217.181:8080",
        changeOrigin: true,
        secure: false,
        ws: true,
      },
      "/file": {
        target: "http://52.76.217.181:8080",
        changeOrigin: true,
        secure: false,
        ws: false,
      },
    },
  },
});
