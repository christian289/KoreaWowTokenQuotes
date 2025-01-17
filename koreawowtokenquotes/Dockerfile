# npm i 와 npm install은 동일한 명령어다.
# npm ci 와 npm clean-install은 동일한 명령어다.
# npm i 와 npm ci의 차이점은 package-lock.json이 있을 때와 없을 때이다.
# package-lock.json은 npm ci를 할 때 이용되는 파일로 이 파일에 명시된 버전과 모듈만을 사용하여 빌드하기에 다양한 환경에서의 같은 빌드 환경을 유지할 수 있다.
# 

FROM node:18-alpine AS base
WORKDIR /app

# Dependencies
FROM base AS deps
COPY package.json package-lock.json ./
RUN npm ci

# Builder
FROM base AS builder
COPY --from=deps /app/node_modules ./node_modules
COPY . .
RUN npm run build

# Runner
FROM base AS runner
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package.json ./package.json
COPY --from=builder /app/public ./public

EXPOSE 3000
CMD ["npm", "start"]
