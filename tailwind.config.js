/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx,*.js}",
    "./node_modules/flowbite/**/*.js"
  ],
  theme: {
    extend: {
       zIndex: {
        '90': '90',
      }
    },
  },
  plugins: [
    require('flowbite'),
    require('tailwindcss-animated')
  ]
}
