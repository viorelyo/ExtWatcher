import React from 'react';
import { Dimmer, Loader } from 'semantic-ui-react';

const MainLoader = () => (
    <Dimmer active>
        <Loader size="large">Loading</Loader>
    </Dimmer>
);
  
export default MainLoader;