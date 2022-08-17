import { css } from 'styled-components';

export const typoStyles = css`
  @font-face {
    font-family: 'Pampers Script';
    src: url('../fonts/PampersScript_pnabby.otf') format('opentype');
    font-display: swap;
  }
  @font-face {
    font-family: 'Memimas Pro Bold';
    src: url('../fonts/MemimasPro-Bold.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Harmonia Sans Pro Regular';
    src: url('../fonts/HarmoniaSansPro-Regular_toooux.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Harmonia Sans Std Regular';
    src: url('../fonts/HarmoniaSansStd-Regular.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Harmonia Sans Pro Light';
    src: url('../fonts/HarmoniaSansPro-Light.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Harmonia Sans Std Regular';
    font-weight: 600;
    src: url('../fonts/HarmoniaSansStd-SemiBd.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Harmonia Sans Std Regular';
    font-weight: 700;
    src: url('../fonts/HarmoniaSansStd-Bold.otf') format('opentype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Lato Regular';
    src: url('../fonts/Lato-Regular_yqe25g.ttf') format('truetype');
    font-display: swap;
  }

  @font-face {
    font-family: 'Lato Regular';
    font-weight: 700;
    src: url('../fonts/Lato-Bold_rsyuyj.ttf') format('truetype');
    font-display: swap;
  }

  /* latin-ext */
  @font-face {
    font-family: 'Lato Regular';
    font-style: italic;
    font-weight: 400;
    src: url('../fonts/Lato-Italic_mdxrjp.ttf') format('truetype');
    unicode-range: U+0100-024F, U+0259, U+1E00-1EFF, U+2020, U+20A0-20AB, U+20AD-20CF, U+2113, U+2C60-2C7F, U+A720-A7FF;
    font-display: swap;
  }

  /* latin */
  @font-face {
    font-family: 'Lato Regular';
    font-style: italic;
    font-weight: 400;
    src: url('../fonts/Lato-Italic_mdxrjp.ttf') format('truetype');
    unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC,
      U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
    font-display: swap;
  }

  /* latin-ext */
  @font-face {
    font-family: 'Lato Regular';
    font-style: normal;
    font-weight: 700;
    font-display: swap;
    src: url('../fonts/Lato-Bold_rsyuyj.ttf') format('truetype');
    unicode-range: U+0100-024F, U+0259, U+1E00-1EFF, U+2020, U+20A0-20AB, U+20AD-20CF, U+2113, U+2C60-2C7F, U+A720-A7FF;
  }
  /* latin */
  @font-face {
    font-family: 'Lato Regular';
    font-style: normal;
    font-weight: 700;
    font-display: swap;
    src: url('../fonts/Lato-Bold_rsyuyj.ttf') format('truetype');
    unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02BB-02BC, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC,
      U+2122, U+2191, U+2193, U+2212, U+2215, U+FEFF, U+FFFD;
  }

  @font-face {
    font-family: 'El Messiri Regular';
    src: url('../fonts/elmessiri-regular.otf') format('opentype');
    font-display: swap;
  }

  body {
    font-family: var(--font-primary), Arial, Helvetica, sans-serif;
  }

  .font-primary {
    font-family: var(--font-primary), Arial, sans-serif;
  }

  .font-primary-pro {
    font-family: var(--font-primary-pro), Arial, sans-serif;
  }

  .font-primary-light {
    font-family: var(--font-primary-light), Arial, sans-serif;
  }

  .font-secondary {
    font-family: var(--font-secondary), Arial, sans-serif;
  }

  .font-pampers {
    font-family: var(--font-pampers), Arial, sans-serif;
  }

  .font-pure {
    font-family: var(--font-pure), Arial, sans-serif;
  }

  h5 {
    font-weight: normal;
  }
`;

export default typoStyles;
