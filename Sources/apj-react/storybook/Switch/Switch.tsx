import React from 'react';
// import {Helmet} from "react-helmet";
import styled from 'styled-components';
import bootstapGrid from '../base/bootstrapGrid';
import bootstapReboot from '../base/bootstrapReboot';
import { ColorStyles } from '../../styles/colors';
import typography from '../base/typography';
import defaultFonts from '../base/defaultFonts';
import customFonts from '../base/customFonts';

export interface SwitchProps {
  children: any;
}

const cleanup = (idName: string) => {
  const elem = document.getElementById(idName);
  /* eslint-disable */
      if (elem != null || elem != undefined) {
          if(elem.parentNode){
              elem.parentNode.removeChild(elem);
          }
      }
      /* eslint-enable */
};

const appender = (styles: any, idName: string) => {
  cleanup(idName);
  const styleElement = document.createElement('style');
  styleElement.setAttribute('id', idName);
  styleElement.appendChild(document.createTextNode(styles));
  document.head.appendChild(styleElement);
};

const Switch: React.FC<SwitchProps> = ({ children }) => {

  React.useEffect(() => {
    appender(bootstapGrid, 'bootstrap-inject-style');
    appender(defaultFonts, 'fonts-inject-style');
    appender(ColorStyles, 'colors-inject-style');
    // add all typo
    appender(typography, 'typography-inject-style');
  }, []);
  
  return <StyleWrapper>{children}</StyleWrapper>;
};

const StyleWrapper = styled.div`
`;

export default Switch;
