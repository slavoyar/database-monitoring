const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin');
const path = require('path');

let mode = 'development';
if (process.env.NODE_ENV === 'production') {
  mode = 'production';
}

const isProduction = mode === 'production';
const devtool = isProduction ? false : 'inline-source-map';

module.exports = {
  entry: './src/index.tsx',
  mode,
  devtool,
  plugins: [
    new MiniCssExtractPlugin(),
    new HtmlWebpackPlugin({
      template: './public/index.html',
      filename: './index.html',
      favicon: './public/favicon.ico',
      manifest: './public/manifest.json',
    }),
  ],
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
      },
      {
        test: /\.(ts|tsx)$/,
        use: {
          loader: 'ts-loader',
          options: {
            compilerOptions: {
              noEmit: false,
            },
          },
        },
      },
      {
        test: /\.(html)$/,
        loader: 'html-loader',
      },
      {
        test: /\.(s[ac]|c)ss$/i,
        use: [MiniCssExtractPlugin.loader, 'css-loader'],
      },
      {
        test: /\.(png|svg|jpg|jpeg|gif|ico|json)$/,
        exclude: /node_modules/,
        use: ['file-loader?name=[name].[ext]'],
      },
    ],
  },
  resolve: {
    plugins: [
      new TsconfigPathsPlugin({
        baseUrl: path.resolve(__dirname, './'),
        configFile: path.resolve(__dirname, './tsconfig.json'),
        extensions: ['.js', '.ts', '.tsx'],
      }),
    ],
    extensions: ['.js', '.jsx', '.ts', '.tsx', '.css', '.json'],
  },
  output: {
    filename: 'bundle.js',
    path: path.resolve(__dirname, 'dist'),
    clean: true,
    publicPath: '/',
  },
  devServer: {
    webSocketServer: isProduction ? false : 'ws',
    hot: !isProduction,
    static: {
      directory: path.join(__dirname, 'public'),
    },
    historyApiFallback: true,
    proxy: [
      {
        context: ['/api/auth', '/api/users'],
        target: 'http://localhost:5000/',
      },
      {
        context: ['/api/server', '/api/log', '/serverState'],
        target: 'http://localhost:5002/',
      },
      {
        context: ['/api/v1/workspace', '/api/v1/users', '/api/v1/servers'],
        target: 'http://localhost:5009/',
      },
      {
        context: ['/api/notification'],
        target: 'http://localhost:5001/',
      },
    ],
  },
  performance: {
    hints: false,
    maxEntrypointSize: 512000,
    maxAssetSize: 512000,
  },
};
