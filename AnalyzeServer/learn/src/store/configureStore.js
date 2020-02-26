import { applyMiddleware, compose, createStore } from "redux";
import reducer from "./reducers";
import createSagaMiddleware from "redux-saga";
// import logger from "redux-logger";
import rootSaga from "./sagas";

export function configureStore() {
  const sagaMiddleware = createSagaMiddleware();
  const composeEnhancers =
    window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
  const store = createStore(
    reducer,
    // composeEnhancers(applyMiddleware(logger, sagaMiddleware))
    composeEnhancers(applyMiddleware(sagaMiddleware))
  );

  sagaMiddleware.run(rootSaga);
  return store;
}
