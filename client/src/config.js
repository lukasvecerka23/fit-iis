const development = {
    apiUrl: 'https://localhost:7246',
    // other development settings
};

const production = {
    apiUrl: 'https://myproductionapi.com',
    // other production settings
};

const config = process.env.NODE_ENV === 'production' ? production : development;

export default config;