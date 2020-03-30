#-------------------Download Base Image----------------------------------#

FROM node:13.12.0 as build

#------------------Set Working Directory---------------------------------#

WORKDIR /app

#------------------Copy Package Files to App-----------------------------#

COPY package*.json ./

#------------------Install Dependencies----------------------------------#

RUN npm install

#------------------Install Angular/CLI-----------------------------------#

RUN npm install -g @angular/cli

#------------------Copy App----------------------------------------------#

COPY . .

#------------------Expose Port-------------------------------------------#

EXPOSE 4200

#------------------Run App-----------------------------------------------#

CMD ng serve --host 0.0.0.0 --port 4200

#------------------PROD Build--------------------------------------------#

# RUN ng build --output-path=dist
# FROM nginx:1.16.0-alpine
# COPY --from=build /app/dist /usr/share/nginx/html
# EXPOSE 80
# CMD ["nginx", "-g", "daemon off;"]

#------------------End---------------------------------------------------#