export function userReducer(state=null,action){
    switch(action.type){
        case "LOGIN":
            return action.payload;
            // return "LOGIN";
        case "LOGOUT":
            return "Logout"
        default:
            return state;
    }
}