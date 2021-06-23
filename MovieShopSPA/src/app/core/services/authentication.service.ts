import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable,BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';
import { UserLogin } from 'src/app/shared/models/userLogin';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private user!: User;
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public CurrentUser = this.currentUserSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) { }


  login(userLogin: UserLogin): Observable<boolean> {
    // take the userLogin object from login component and send it to API
    // if api validates un/pw return token
    // store that token in localstorage return true
    // return false;
    // https://localhost:44368/api/Account/login

    return this.http.post(`${environment.apiUrl}${'Account/login'}`, userLogin)
      .pipe(map((response: any) => {
        console.log(response);
        localStorage.setItem('jwtToken', response.token);
        this.populateUserData();
        return true;
      }
      ));

  }


  populateUserData()
{
  // get the token from localstorage and decode the token and convert to User object and push it to currentUserSubject
  const helper = new JwtHelperService();
  const myJwtToken = localStorage.getItem('jwtToken');

  if (myJwtToken) {
    const decodedToken = helper.decodeToken(myJwtToken);
    this.user = decodedToken;
    this.currentUserSubject.next(this.user);
    this.isAuthenticatedSubject.next(true);
  }
  //get the token from localstroage and decode the token 
}
  logout() {

      // remove the token from local storage
    // clear out the subjects

    localStorage.removeItem('jwtToken');
    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);

  }

}
