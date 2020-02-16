import React from 'react';
import { Dimmer, Loader } from 'semantic-ui-react';

const MainLoader = () => (
    <Dimmer active inverted>
        <Loader inverted size="small" inline="centered">Loading</Loader>
    </Dimmer>
);
  
export default MainLoader;