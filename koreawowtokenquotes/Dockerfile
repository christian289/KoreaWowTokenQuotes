# DockerFile 위치 -> koreawowtokenquotes\Dockerfile
# docker build 명령어 실행 위치(build context) -> koreawowtokenquotes
# docker build 명령어 -> docker build -t wow-token-api -f koreawowtokenquotes.server/Dockerfile .

# npm i 와 npm install은 동일한 명령어다.
# npm ci 와 npm clean-install은 동일한 명령어다.
# npm i 와 npm ci의 차이점은 package-lock.json이 있을 때와 없을 때이다.
# package-lock.json은 npm ci를 할 때 이용되는 파일로 이 파일에 명시된 버전과 모듈만을 사용하여 빌드하기에 다양한 환경에서의 같은 빌드 환경을 유지할 수 있다.
# .NET으로 비교하면 ASP.NET Core 프로젝트를 생성하면 기본적으로 '종속성(Dependencies)'에 Packages와 Framework가 있는데 거기에 기본적으로 내장된 DLL들이
# package-lock.json과 같은 역할이다. 어떤 환경에서 ASP.NET Core 프로젝트를 생성해도 이것들은 기본적으로 프로젝트를 생성하면서 설치가 되는 것이다.
# 따라서 package-lock.json도 기본적으로 설치되는 것들이므로 같은 역할이라고 볼 수 있다.
# ASP.NET Core 프로젝트는 .NET이 업데이트가 될 때마다 업데이트 되므로, 내장된 Nuget Package들은 자주 배포되어 그 사이에 많은 버전업이 일어났을 수도 있다.
# 그래서 ASP.NET Core 프로젝트를 생성한 뒤 최신 Nuget Package들을 다운로드하여 별도로 업데이트할 수 있다.
# .NET에는 별도로 업데이트하는 행위에 대해 따로 기록하지 않지만, next.js에서는 package-lock.json에 설치된 것을 별도로 업데이트할 경우
# package.json에 별도로 다운로드하거나 설치한 것을 기록하여 이것을 동시에 package-lock.json에 함께 업데이트해준다.
# 큰 맥락으로 보면 .NET과 유사하다.

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
