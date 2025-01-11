// @ts-nocheck
import type {NextConfig} from "next";

const nextConfig: NextConfig = {
    webpack: (config) => {
        // camel-case style names from css modules
        config.module.rules
            .find(({oneOf}) => !!oneOf).oneOf
            .filter(({use}) => JSON.stringify(use)?.includes('css-loader'))
            .reduce((acc, {use}) => acc.concat(use), [])
            .forEach(({options}) => {
                if (options.modules) {
                    options.modules.exportLocalsConvention = 'camelCase';
                }
            });

        config.module.rules
            .find(({oneOf}) => !!oneOf).oneOf
            .filter(({use}) => JSON.stringify(use)?.includes('sass-loader'))
            .reduce((acc, {use}) => acc.concat(use), [])
            .forEach(({options}) => {
                if (options.sassOptions) {
                    options.sassOptions.quietDeps = true;
                }
            });
        return config;
    }
};

export default nextConfig;
