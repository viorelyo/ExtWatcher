import { call, fork, take } from "redux-saga/effects";
import * as fileActions from "../actions/file";
import { REQUEST } from "../actions";
import { fetchEntity } from "./index";
