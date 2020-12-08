import { ActionType } from './action-type';

export interface Action {
    type: ActionType; // כל הפעולות שאנחנו מעוניינים לעשות
    payload?: any; // המידע עצמו
}