import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { UserLogin } from 'src/app/shared/models/userLogin';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  userLogin: UserLogin = {

    email: '', password: ''
  };

  invalidLogin!: Boolean;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {

    console.log('inside ngOninit method, checking UserLogin object');
    console.log(this.userLogin);
  }

  login() {
    // console.log('inside login method, checking UserLogin object');
    // console.log('form submitted here');
    // console.log(this.userLogin);
    this.authService.login(this.userLogin).subscribe(
      (response) => {
        if (response) {

          //  redirect to home page
        }
      }
    );

  }

}
