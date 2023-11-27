const development = {
    apiUrl: 'https://localhost:7246',
    // other development settings
};

const production = {
    apiUrl: 'https://app-iis-2023-api.azurewebsites.net',
    // other production settings
};

const config = process.env.NODE_ENV === 'production' ? production : development;

export default config;