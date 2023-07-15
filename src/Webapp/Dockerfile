# Dependencies
FROM node:16.20.0-alpine

# Create app directory
WORKDIR /usr/src/miau/Webapp

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
RUN npm install -g npm@9.8.0
COPY package*.json ./
RUN npm install

# Bundle app source
COPY . .

EXPOSE 8080

CMD ["npm", "run", "start"]