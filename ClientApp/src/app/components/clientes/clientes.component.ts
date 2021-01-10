//import { Component, OnInit } from '@angular/core';
import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Client} from '../../models/Client';
import { ClienteService } from '../../service/cliente.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-clients',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.scss']
})
export class ClientesComponent implements OnInit, OnDestroy {

  public clienteForm: FormGroup;
  public titulo = 'Cliente';
  public clienteSelecionado: Client;
  public textSimple: string;
  public clientes: Client[];
  public cliente: Client;
  public msnDeleteCliente: string;
  public modeSave = 'post';
  private unsubscriber = new Subject();

  constructor(
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private fb: FormBuilder

  ) {
    this.criarForm();
  }

  ngOnInit() {
    this.carregarClientes();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next();
    this.unsubscriber.complete();
  }

  criarForm() {
    this.clienteForm = this.fb.group({
      id: [0],
      tag: ['', Validators.required],
      timestamp: ['', Validators.required],
      value: ['', Validators.required]
    });
  }

  saveCliente() {
    if (this.clienteForm.valid)
    {

      if (this.modeSave === 'post') {
        this.cliente = {...this.clienteForm.value};
      } else {
        this.cliente = {id: this.clienteSelecionado.id, ...this.clienteForm.value};
      }

      this.clienteService[this.modeSave](this.cliente)
        .pipe(takeUntil(this.unsubscriber))
        .subscribe(
          () => {
            this.carregarClientes();
            alert('Cliente salvo com sucesso!');
          }, (error: any) => {
            alert('Cliente salvo com sucesso!');
            console.error(error);
          }
        );

    }
  }

  carregarClientes() {
    const id = +this.route.snapshot.paramMap.get('id');

    this.clienteService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((clientes: Client[]) => {
        this.clientes = clientes;

        // if (id > 0) {
        //   this.clienteSelect(this.cliente.find(cliente => cliente.id === id));
        // }

        alert('Clientes foram carregado com Sucesso!');
      }, (error: any) => {
        alert('Clientes não carregados!');
        console.log(error);
        console.log(this.clientes);
      }
    );
  }

  clienteSelect(cliente: Client) {
    this.modeSave = 'put';
    this.clienteSelecionado = cliente;
    this.clienteForm.patchValue(cliente);
  }

  voltar() {
    this.clienteSelecionado = null;
  }

}
