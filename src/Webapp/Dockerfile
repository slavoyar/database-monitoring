# Dependencies
FROM node:16.20.0-alpine

# Create app directory
WORKDIR /usr/src/miau/Webapp

# Install app dependencies
RUN npm install -g npm@9.8.0
COPY package*.json ./
RUN npm install

# Bundle app source
COPY . .

CMD ["npm", "run", "build-prod"]