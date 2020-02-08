const toggleMenu = 'TOGGLE_MENU';
const initialState = { smallMenu: true };

export const actionCreators = {
  toggleSideMenu: () => ({ type: toggleMenu }),
};

export const reducer = (state, action) => {
  state = state || initialState;

  if (action.type === toggleMenu) {
    return { ...state, smallMenu: !state.smallMenu};
  }

  return state;
};