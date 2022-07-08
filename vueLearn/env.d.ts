/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_DOTNET_API: string,
    readonly VITE_NODE_API: string,
    readonly VITE_BASE: string
}

interface ImportMeta {
    readonly env: ImportMetaEnv
}