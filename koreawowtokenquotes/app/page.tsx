// "use client";

// export default function Home() {
//   const clientId = "your-client-id"; // 실제 발급받은 값으로 교체하세요
//   const redirectUri = "http://localhost:3000/api/auth/callback";

//   const authUrl = `https://oauth.battle.net/authorize?response_type=code&client_id=${clientId}&redirect_uri=${encodeURIComponent(
//     redirectUri
//   )}`;

//   return (
//     <div className="bg-black text-gold min-h-screen flex flex-col items-center justify-center">
//       <h1 className="text-3xl font-bold">Battle.net OAuth Integration</h1>
//       <a
//         href={authUrl}
//         className="mt-5 px-6 py-3 bg-gold text-black font-bold rounded"
//       >
//         Login with Battle.net
//       </a>
//     </div>
//   );
// }

import TokenData from './components/TokenData';

export default function Home() {
  return (
    <main>
      <TokenData />
    </main>
  );
}
