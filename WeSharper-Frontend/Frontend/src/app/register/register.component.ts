import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerMode = false;
  model: any = {}
  registerForm: FormGroup;

  constructor(private http: HttpClient, 
        private accountService: AccountService, 
        private router: Router, 
        private toastr: ToastrService,
        private fb: FormBuilder) { }

  ngOnInit(): void {
      this.initializeForm();
  }

  initializeForm(){
    this.registerForm = this.fb.group({
        username:['', Validators.required],
        firstName: ['',Validators.required],
        lastName: ['',Validators.required],
        email: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(8)]],
        confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    })
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
        this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
      return (control: AbstractControl) => {
        return control?.value === control?.parent?.controls[matchTo].value 
            ? null : { isMatching: true}
      }
  }


  register(){
    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/feeds')
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

}
